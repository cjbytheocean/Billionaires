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
    // Takes radii of Billionaires and Billions and ensure there is no overlap, additionally,
    // avoids billions spawning in random places.

    void BillionSpawn()
    {
        CircleCollider2D billionaireCollider = GetComponent<CircleCollider2D>();
        CircleCollider2D billionCollider = billionPrefab.GetComponent<CircleCollider2D>();

        float billionaireRadius = billionaireCollider.radius * transform.localScale.x;
        float billionRadius = billionCollider.radius * billionPrefab.transform.localScale.x;

        Vector2 position;

        // If there are no billions spawned on a billionaire, this ensures it spawns next to the billionaire
        // and not somewhere random. Since this is a 2d game, casting to Vector2 removes Z axis.
        // Else, ensure the billion spawns next to the last spawned billion, but allowing no overlap to it or the billionaire.

        if (spawnedBillions.Count == 0)
        {
            position = (Vector2)transform.position +  spawnDirection.normalized * (billionaireRadius + billionRadius + spacing);
        } 
        else
        {
            GameObject lastSpawned = spawnedBillions[^1];
            float lastBillionRadius = lastSpawned.GetComponent<CircleCollider2D>().radius * billionPrefab.transform.localScale.x;
            position = (Vector2)transform.position +  spawnDirection.normalized * (billionaireRadius + billionRadius + spacing);
        }

        // Creates a new billion at runtime, sets SpriteRenderer to the color of the base determined in the inspector.
        // Then, adds billion to the list of spawned billions.
        // Used Quaternion from lab, I had to research more about how they work.

        GameObject billion = Instantiate(billionPrefab, position, Quaternion.identity);
        SpriteRenderer coloring = billion.GetComponent<SpriteRenderer>();
        coloring.color = billionaireColor;
        spawnedBillions.Add(billion);
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
