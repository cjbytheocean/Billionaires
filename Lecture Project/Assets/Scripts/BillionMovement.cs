using System;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BillionMovement : MonoBehaviour
{
    float maxSpeed = 5f;
    float accSpeed = 7f;
    float slowDownWithinRange = 1.5f;
  
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject flag;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0.1f;
    }

    // ai used to help with vector math, i was not sure what to do
    void FixedUpdate()
    {

        if (flag == null)
        {
            flag = FindNearestFlag();
        }

        if (flag == null)
        {
            return;
        }
        
        Vector2 v = (Vector2)flag.transform.position - rb.position;
        float dist = v.magnitude;
        Vector2 d = v.normalized;

        float slowDownSpeed = maxSpeed;
        if (dist < slowDownWithinRange)
        {
            slowDownSpeed = maxSpeed * (dist / slowDownWithinRange);
        }

        Vector2 dv = d * slowDownSpeed;
        Vector2 s = dv - rb.linearVelocity;
        s = Vector2.ClampMagnitude(s, accSpeed * Time.fixedDeltaTime);
        rb.AddForce(s);
    }

    // finds the nearest flag to each respective billion

    
    GameObject FindNearestFlag()
    {
        Debug.Log("FindNearestFlag() called");
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");
        GameObject closestFlag = null;
        float dist = float.MaxValue;

        foreach (GameObject flag in flags)
        {
            // help with this line
            if (flag.TryGetComponent<SpriteRenderer>(out var individualSR))
            {
                Debug.Log("loop reached");
                if (CheckColor(individualSR.color, sr.color))
                {
                    Debug.Log("loop checked");
                    Debug.Log($"Billion: {sr.color} Flag: {individualSR.color}");
                    float localDist = Vector2.Distance(transform.position, flag.transform.position);

                    if (localDist < dist)
                    {
                        dist = localDist;
                        closestFlag = flag;
                    }  
                }
            }
        }
        Debug.Log("if loop not checked skip");
        return closestFlag;
    }

    // helper method to help detect color 
    bool CheckColor(Color one, Color two, float delta = 0.05f)
    {
        return Mathf.Abs(one.r - two.r) < delta && Mathf.Abs(one.g - two.g) < delta && Mathf.Abs(one.b - two.b) < delta;
    }
}
