using System.Collections.Generic;
using System.Xml.XPath;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

public class BaseProcGen : MonoBehaviour
{
    [SerializeField] float wallMargin = 10f;
    [SerializeField] float baseMargin = 10f;

    public GameObject[] billionairePrefabs;
    List<Vector2> spawnPositions = new List<Vector2>();

    // public Vector2 minimum;
    // public Vector2 maximum;

    public ProcGenWalker pgw;

    IEnumerator Start()
    {
        while (!pgw.FinishedLoading)
        {
            yield return null;
        }
        BaseGeneration();
    }

    // Spawns bases at determined valid spawn position for each base. When position is determined, instantiates
    // the prefabs at those positions and gets their needed components.

    void BaseGeneration()
    {
        spawnPositions.Clear();

        int t = 0; // this is to help prevent an infinite loop

        while (spawnPositions.Count < 4 && t < 5000)
        {
            t++;

            float xPosition = Random.Range(pgw.mapMinimum.x + wallMargin, pgw.mapMaximum.x - wallMargin);
            float yPosition = Random.Range(pgw.mapMinimum.y + wallMargin, pgw.mapMaximum.y - wallMargin);

            Vector2 spawned = new Vector2(xPosition, yPosition);

            int x = Mathf.RoundToInt(spawned.x);
            int y = Mathf.RoundToInt(spawned.y);

            if (WithinConstraints(spawned) && pgw.gridHandler[x, y] == ProcGenWalker.Grid.FLOOR)
            {
                spawnPositions.Add(spawned);
            } 
        }

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            GameObject billionaire = Instantiate(billionairePrefabs[i], spawnPositions[i], Quaternion.identity);

            BillionSpawner bs = billionaire.GetComponent<BillionSpawner>();
            BillionaireExperience be = billionaire.GetComponentInChildren<BillionaireExperience>();

            if (bs != null)
            {
                bs.xpBase = be;

                SpriteRenderer sr = billionaire.GetComponent<SpriteRenderer>();
                bs.billionaireColor = sr.color;
            }
        } 
    }

    // Helper method to check if the generated position is within the bounds of the vectors.

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
}
