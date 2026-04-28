using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Data;
using Unity.Collections;
using System;
using TMPro;

public class BillionSpawner : MonoBehaviour
{ 
    public GameObject billionPrefab;
    public Color billionaireColor;
    public float spawnInterval = 5f;
    public float spacing = 0.01f;
    public Vector2 spawnDirection = Vector2.right;

    public Vector3 basePosition;

    private List<GameObject> spawnedBillions = new List<GameObject>();
    private int i = 0;

    public BillionaireExperience xpBase;

    void Start()
    {
        basePosition = transform.position;
        InvokeRepeating(nameof(BillionSpawn), spawnInterval, spawnInterval); 
    }
    
    public void BillionSpawn()
    {
       
        CircleCollider2D billionaireCollider = GetComponent<CircleCollider2D>();
        CircleCollider2D billionCollider = billionPrefab.GetComponent<CircleCollider2D>();

        float billionaireRadius = billionaireCollider.radius * transform.localScale.x;
        float billionRadius = billionCollider.radius * billionPrefab.transform.localScale.x;
        float dist = billionaireRadius + billionRadius + spacing + 0.05f;
     
        float radius = dist + 0.1f;
        Vector2 offset = UnityEngine.Random.insideUnitCircle * radius;
        Vector3 pos = transform.position + (Vector3) offset;
      
        GameObject billion = Instantiate(billionPrefab, pos, Quaternion.identity);

        TurretBehavior tb = billion.GetComponent<TurretBehavior>();
        tb.billionaireColor = billionaireColor;
        tb.shooterBase = xpBase;
        billion.GetComponent<SpriteRenderer>().color = billionaireColor;
        
        TextMeshPro text = billion.GetComponentInChildren<TextMeshPro>();
        text.text = xpBase.rank.ToString();
  
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
