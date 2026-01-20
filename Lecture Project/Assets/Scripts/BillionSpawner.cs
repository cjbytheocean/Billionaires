using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Data;
using Unity.Collections;

public class BillionSpawner : MonoBehaviour
{
    // Variables to store the billions and their respective color according to the billionaire.
    // Variables for spawn interval, spacing, and spawn direction to ensure the billions don't touch each other or the billionaire. 
    public GameObject billionPrefab;
    public Color billionaireColor;
    public float spawnInterval = 5f;
    public float spacing = 0.01f;
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
        float diameter = billionRadius * 2f + spacing;

        int r = 0;
        int num = i;

        while (num >= r + 1)
        {
            num -= r + 1;
            r++;
        }

        int c = num;

        // Vector2 Parameters created using a "triangular" cluster to pack closer together
        // Horizontal spacing scaled by sqrt3/2

        float x = dist + r * (diameter * 0.87f);
        float y = (c - r * 0.5f) * diameter;

        Vector2 offset = new Vector2(x, y);
      
        // Creates a new billion at runtime, sets SpriteRenderer to the color of the base determined in the inspector.
        // Then, adds billion to the list of spawned billions.
        // Used Quaternion from lab, I had to research more about how they work.
        // I did not realize that Instantiate needs a 3D conversion.

        Vector3 position3D = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, 0f);
        GameObject billion = Instantiate(billionPrefab, position3D, Quaternion.identity);
       
        SpriteRenderer coloring = billion.GetComponent<SpriteRenderer>();
        coloring.color = billionaireColor;
        spawnedBillions.Add(billion);
        i++;
    }
}
