using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public Transform turret;
    public Color billionaireColor;
    public static List<GameObject> billions = new List<GameObject>(); 
    float storedAngle;

    public GameObject blaster;
    public float blasterInterval = 1.0f;
    public float blasterDistance = 3f;
    public Transform blasterFirePoint;
    float blasterFireTimer;

    public bool billionaire;

    void Awake()
    {
        billions.Add(gameObject);
    }

    void OnDestroy()
    {
        billions.Remove(gameObject);
    }

    // https://docs.unity3d.com/560/Documentation/ScriptReference/MonoBehaviour.LateUpdate.html
    // If there are several opponents, call ClosestBillion(). Otherwise, choose to rotate
    // turret based on last stored angle.
    // Updated for checkpoint 5

    void LateUpdate()
    {
        GameObject t = ClosestBillion();

        if (t != null)
        {
            RotateTurret(t.transform);
            float dist = Vector3.Distance(transform.position, t.transform.position);

            if (dist <= blasterDistance)
            {
                blasterFireTimer += Time.deltaTime;

                if (blasterFireTimer >= blasterInterval)
                {
                    blasterFireTimer = 0;
                    GameObject billionBlaster = Instantiate(blaster, blasterFirePoint.position, blasterFirePoint.rotation);

                    BlasterBehavior bb = billionBlaster.GetComponent<BlasterBehavior>();
                    bb.blasterColor = billionaireColor;

                    SpriteRenderer sr = billionBlaster.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.color = billionaireColor;
                    }
                }
            }
            else
            {
                blasterFireTimer = 0;
            }
        }
        else
        {
            turret.localRotation = Quaternion.Euler(0, 0, storedAngle);
        }
    }

    // Finds and returns the closest billion in respect to another billion. If the billion is itself or
    // a billion of the same color, continue to calculate the closest distance until the closest opponent is located.

    GameObject ClosestBillion()
    {
        GameObject closestBillion = null;
        float dist = Mathf.Infinity;


        foreach (GameObject billion in billions)
        {
            TurretBehavior tb = billion.GetComponent<TurretBehavior>();

            if (billion == gameObject || billion == null)
            {
                continue;
            }

            if (tb.billionaire == true && tb.billionaireColor == billionaireColor)
            {
                continue;
            }


            if (tb.billionaire == false && tb.billionaireColor != billionaireColor)
            {
                float dst = Vector3.Distance(transform.position, billion.transform.position);

                if (dst < dist)
                {
                    dist = dst;
                    closestBillion = billion;
                }
            }
        }
        return closestBillion;
    }

    // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Quaternion.Euler.html
    // Instead of just y/x, arc tangent will account for quadrants
    // Stores last angle in the case of no opponents, then rotates turret according to calculation.
    void RotateTurret(Transform tb)
    {
        Vector2 pointDirection = tb.position - transform.position;
        float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;
        storedAngle = angle + 90f;
        turret.localRotation = Quaternion.Euler(0, 0, angle + 90f);
    }
}
