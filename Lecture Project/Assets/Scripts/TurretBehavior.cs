using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Experimental.GraphView;
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


        foreach (GameObject billion in billions)
        {
            TurretBehavior tb = billion.GetComponent<TurretBehavior>();
            if (tb.bs.billionaireColor == bs.billionaireColor)
            {
                continue;
            }

            float dst = Vector3.Distance(transform.position, billion.transform.position);

            if (dst < dist)
            {
                dst = dist;
                closestBillion = billion;
            }
        }
        return closestBillion;
    }

    // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Quaternion.LookRotation.html
    void RotateBillion(Transform tb)
    {
        Vector3 pointDirection = turret.position - tb.position;
        pointDirection.y = 0f;

        if (pointDirection != Vector3.zero)
        {
            turret.rotation = Quaternion.LookRotation(pointDirection);
        }
    }
}
