using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseProcGen : MonoBehaviour
{

    [SerializeField] float wallMargin = 10f;
    [SerializeField] float baseMargin = 10f;

    public GameObject[] billionairePrefabs;
    List<Vector2> spawnPositions = new List<Vector2>();

    void Start()
    {
        BaseGeneration();
    }

    void BaseGeneration()
    {
        spawnPositions.Clear();

        int t = 0; // i am using this to prevent an infinite loop, might not be needed ? 

        while (spawnPositions.Count < 4 && t < 500)
        {
            t++;

            float xPosition = 0.0f;
            float yPosition = 0.0f;

            Vector2 spawned = new Vector2(xPosition, yPosition);

            if (WithinConstraints(spawned))
            {
                spawnPositions.Add(spawned);
            } 
        }

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            Instantiate(billionairePrefabs[i], spawnPositions[i], Quaternion.identity);
        } 
    }

    bool WithinConstraints(Vector2 v)
    {
        return false; 
    }
}
