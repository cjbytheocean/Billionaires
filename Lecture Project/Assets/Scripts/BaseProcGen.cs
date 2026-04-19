using System.Collections.Generic;
using System.Xml.XPath;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.Experimental.AI;

public class BaseProcGen : MonoBehaviour
{
    [SerializeField] float wallMargin;
    [SerializeField] float baseMargin;

    public GameObject[] billionairePrefabs;
    List<Vector2> spawnPositions = new List<Vector2>();

    public ProcGenWalker pgw;

    IEnumerator Start()
    {
        while (!pgw.FinishedLoading)
        {
            yield return null;
        }
        BaseGeneration();
    }

    void BaseGeneration()
    {
        spawnPositions.Clear();

        int t = 0; // this is to help prevent an infinite loop

        while (spawnPositions.Count < 4 && t < 7000000)
        {
            t++;

            // float xPosition = Random.Range(pgw.mapMinimum.x + wallMargin, pgw.mapMaximum.x - wallMargin);
            // float yPosition = Random.Range(pgw.mapMinimum.y + wallMargin, pgw.mapMaximum.y - wallMargin);

            // Vector2 spawned = new Vector2(xPosition, yPosition);

            Camera c = Camera.main;

            Vector3 v = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Mathf.Abs(c.transform.position.z));
            Vector3 w = c.ViewportToWorldPoint(v);
            Vector2 spawned = new Vector2(w.x, w.y);

            

            int x = Mathf.RoundToInt(spawned.x);
            int y = Mathf.RoundToInt(spawned.y);

            if (WithinConstraints(spawned) && pgw.gridHandler[x, y] == ProcGenWalker.Grid.FLOOR && CheckWallDistance(x, y, 2.35f))
            {
                spawnPositions.Add(spawned);
            } 
        }

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            GameObject billionaire = Instantiate(billionairePrefabs[i], spawnPositions[i], Quaternion.identity);

            BillionSpawner bs = billionaire.GetComponent<BillionSpawner>();
            BillionaireExperience be = billionaire.GetComponentInChildren<BillionaireExperience>();

            TurretBehavior tb = billionaire.GetComponent<TurretBehavior>();
            if (tb != null)
            {
                tb.shooterBase = be;
            }

            if (bs != null)
            {
                bs.xpBase = be;

                SpriteRenderer sr = billionaire.GetComponent<SpriteRenderer>();
                bs.billionaireColor = sr.color;
            }
        } 
    }

    bool WithinConstraints(Vector2 v)
    {
        foreach (Vector2 sp in spawnPositions)
        {
            if (Vector2.Distance(v, sp) < baseMargin)
            {
                return false;
            }
        }
        return true; 
    }

    bool CheckWallDistance(int x, int y, float radius)
    {
        for (float dx = -radius; dx <= radius; dx ++)
        {
            for (float dy = -radius; dy <= radius; dy++)
            {
                float xDistance = x + dx;
                float yDistance = y + dy;

                if (xDistance < 0 || yDistance < 0 || xDistance >= pgw.gridHandler.GetLength(0) || yDistance >= pgw.gridHandler.GetLength(1))
                {
                    continue;
                }

                if (pgw.gridHandler[(int) xDistance, (int) yDistance] == ProcGenWalker.Grid.WALL)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
