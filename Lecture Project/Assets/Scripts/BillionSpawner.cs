using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Data;
using Unity.Collections;
using System;

public class BillionSpawner : MonoBehaviour
{
    // Variables to store the billions and their respective color according to the billionaire.
    // Variables for spawn interval, spacing, and spawn direction to ensure the billions don't touch each other or the billionaire. 
    public GameObject billionPrefab;
    public Color billionaireColor;
    public float spawnInterval = 5f;
    public float spacing = 0.01f;
    public Vector2 spawnDirection = Vector2.right;

    public Vector3 basePosition;

    // Stores spawned billions in a list 
    private List<GameObject> spawnedBillions = new List<GameObject>();
    private int i = 0;

    public BillionaireExperience xpBase;

    // Runs on start, but in this case, functioning to repetitively spawn the billions on the given spawn interval.
    // Using an InvokeRepeat to allow spawning to happen repeatedly. Looked up how to implement InvokeRepeating().
    void Start()
    {
        basePosition = transform.position;
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

        Vector3 pos;
        GameObject currentFlag = FindFlag(billionaireColor);

        if (currentFlag != null && spawnedBillions.Count > 0)
        {
            pos = basePosition;
        }
        else
        {
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

            Vector2 offset = new Vector2(x * spawnDirection.x, y);
            pos = transform.position + (Vector3)offset;
        }
      
        // Creates a new billion at runtime, sets SpriteRenderer to the color of the base determined in the inspector.
        // Then, adds billion to the list of spawned billions.
        // Used Quaternion from lab, I had to research more about how they work.
        // I did not realize that Instantiate needs a 3D conversion.
        // CHANGES HERE FOR CHECKPOINT 4: billions know their color by setting variable in turret script and using that
        // information to not attack each other
        
        // Gets BillionaireExperience component and assigns the experienceBar image accordingly.

        GameObject billion = Instantiate(billionPrefab, pos, Quaternion.identity);

        TurretBehavior tb = billion.GetComponent<TurretBehavior>();
        tb.billionaireColor = billionaireColor;
        tb.shooterBase = xpBase;
        tb.rank = xpBase.rank;
        billion.GetComponent<SpriteRenderer>().color = billionaireColor;

        // Shrinking Billion based on rank

        float multiplier = 0.3f - (tb.rank/10);
        multiplier = Mathf.Max(multiplier, 0.2f);
        billion.transform.localScale -=  multiplier;

        BillionaireExperience be = billion.GetComponent<BillionaireExperience>();
        if (be != null)
        {
            BillionaireExperience bd = GetComponent<BillionaireExperience>();
            if (bd != null)
            {
                be.experienceBar = bd.experienceBar;
            } 
        }

        spawnedBillions.Add(billion);
        i++;
    }

    // This code is largely reused from BillionMovement.cs to find flags. Without this, players were able to 
    // drag the billions and bases as well as the flags.
    GameObject FindFlag(Color c)
    {
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");

        foreach (GameObject flag in flags)
        {
            if (flag.TryGetComponent<SpriteRenderer>(out var sr))
            {
                if (Mathf.Abs(sr.color.r - c.r) < 0.05f && Mathf.Abs(sr.color.g - c.g) < 0.05f &&Mathf.Abs(sr.color.b - c.b) < 0.05f)
                {
                    return flag;
                }
            }
        }
        return null;
    }
}
