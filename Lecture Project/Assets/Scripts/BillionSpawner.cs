using UnityEngine;
using System.Collections.Generic; 

public class BillionSpawner : MonoBehaviour
{
    // Variables to store the billions and their respective color according to the billionaire.
    // Variables for spawn interval and spacing to ensure the billions don't touch each other or the billionaire. 
    public GameObject billionPrefab;
    public Color billionaireColor;
    public float spawnInterval = 2f;
    public float spacing = 0.05f;



    

    

    // Runs on start, but in this case, functioning to repetitively spawn the billions on the given spawn interval.
    // Using an InvokeRepeat to allow spawning to happen repeatedly. Looked up how to implement InvokeRepeating().
    void Start()
    {
        
    }

    // Handles the spawning of the Billions.

    void BillionSpawn()
    {
        
    }

    // Remaining empty for now, as I do not yet know how spawnning is controlled in Billionaires.

    public void StopSpawn()
    {
        
    }

    public void ContinueSpawn()
    {
        
    }


}
