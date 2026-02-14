using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public Transform turret;
    public Color billionaireColor;
    public static List<GameObject> billions = new List<GameObject>(); 

    void Awake()
    {
        billions.Add(gameObject);
    }

    void OnDestroy()
    {
        billions.Remove(gameObject);
    }

    // https://docs.unity3d.com/560/Documentation/ScriptReference/MonoBehaviour.LateUpdate.html

    void LateUpdate()
    {
        GameObject t = ClosestBillion();

        if (t != null)
        {
            RotateBillion(t.transform);
            Debug.DrawLine(transform.position, t.transform.position, Color.red);
        }
    }

    GameObject ClosestBillion()
    {
        GameObject closestBillion = null;
        float dist = Mathf.Infinity;


        foreach (GameObject billion in billions)
        {
            TurretBehavior tb = billion.GetComponent<TurretBehavior>();
            
            if (billion == gameObject)
            {
                continue;
            }

            if (tb.billionaireColor == billionaireColor)
            {
                continue;
            }

            float dst = Vector3.Distance(transform.position, billion.transform.position);

            if (dst < dist)
            {
                dist = dst;
                closestBillion = billion;
            }
        }
        return closestBillion;
    }

    // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Quaternion.Euler.html

    // Instead of just y/x, arc tangent will account for quadrants
    void RotateBillion(Transform tb)
    {
        Vector2 pointDirection = tb.position - transform.position;
        float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;
        turret.localRotation = Quaternion.Euler(0, 0, angle + 90f);
    }
}
