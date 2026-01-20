using UnityEngine;
using System.Collections.Generic; 

public class BillionSpawner : MonoBehaviour
{
    // Variables to store the billions and their respective color according to the billionaire.
    // Variables for spawn interval, spacing, and spawn direction to ensure the billions don't touch each other or the billionaire. 
    public GameObject billionPrefab;
    public Color billionaireColor;
    public float spawnInterval = 2f;
    public float spacing = 0.05f;
    public Vector2 spawnDirection = Vector2.right;

    // Stores spawned billions in a list 
    private List<GameObject> spawnedBillions = new List<GameObject>();


    // Runs on start, but in this case, functioning to repetitively spawn the billions on the given spawn interval.
    // Using an InvokeRepeat to allow spawning to happen repeatedly. Looked up how to implement InvokeRepeating().
    void Start()
    {
        InvokeRepeating(nameof(BillionSpawn), spawnInterval, spawnInterval); 
    }

    // Handles the spawning of the Billions.
    // Takes radii of Billionaires and Billions

    void BillionSpawn()
    {
        CircleCollider2D billionaireCollider = GetComponent<CircleCollider2D>();
        CircleCollider2D billionCollider = billionPrefab.GetComponent<CircleCollider2D>();

        float billionaireRadius = billionaireCollider.radius * transform.localScale.x;
        float billionRadius = billionCollider.radius * billionPrefab.transform.localScale.x;

        Vector2 position;
        
    }

    // Remaining empty for now, as I do not yet know how spawnning is controlled in Billionaires.
    // Placeholders for the assumption that eventually the spawnning will stop and continue.
    // May be deleted later.

    public void StopSpawn()
    {
       
        
    }

    public void ContinueSpawn()
    {
        
    }


}
