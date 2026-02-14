using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public Transform turret;
    public BillionSpawner bs;
    public static List<GameObject> billions = new List<GameObject>(); 
    
    void Awake()
    {
        billions.Add(gameObject);
    }

    void OnDestroy()
    {
        billions.Remove(gameObject);
    }


    void Update()
    {
        GameObject t = ClosestBillion();

        if (t != null)
        {
            RotateBillion(t.transform);
        }
    }

    GameObject ClosestBillion()
    {
        GameObject closestBillion = null;
        float dist = Mathf.Infinity;







        return closestBillion;
    }

    void RotateBillion(Transform tb)
    {
        
    }
}
