using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BillionSpawner : MonoBehaviour
{
    // Variables to store the billions and their respective color according to the billionaire.
    // Variables for spawn interval, spacing, and spawn direction to ensure the billions don't touch each other or the billionaire. 
    public GameObject billionPrefab;
    public Color billionaireColor;
    public float spawnInterval = 5f;
    public float spacing = 0.05f;
    public Vector2 spawnDirection = Vector2.right;

    // Stores spawned billions in a list 
    private List<GameObject> spawnedBillions = new List<GameObject>();

    private int i = 0;


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
        float dist = billionaireRadius + billionRadius + spacing;

        // Cycling through offsets to create triangular spawn pattern
        
        Vector2[] ls =
        {
            new Vector2(1f, 0f),
            new Vector2(0.5f, -0.5f),
            new Vector2(0f, -1f)
        };

        Vector2 offset = ls[i % ls.Length].normalized;
        Vector2 position = (Vector2)transform.position + offset * dist;

        // Creates a new billion at runtime, sets SpriteRenderer to the color of the base determined in the inspector.
        // Then, adds billion to the list of spawned billions.
        // Used Quaternion from lab, I had to research more about how they work.
        // I did not realize that Instantiate needs a 3D conversion.

        Vector3 position3D = new Vector3(position.x, position.y, 0f);
        GameObject billion = Instantiate(billionPrefab, position3D, Quaternion.identity);
       
        SpriteRenderer coloring = billion.GetComponent<SpriteRenderer>();
        coloring.color = billionaireColor;
        spawnedBillions.Add(billion);
        i++;
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
