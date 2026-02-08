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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0;
    }

    // ai used to help with vector math, i was not sure what to do
    void FixedUpdate()
    {
        GameObject moveHere = FindNearestFlag();

        if (moveHere == null)
        {
            return;
        }
        
        Vector2 v = moveHere.transform.position - transform.position;
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
        rb.linearVelocity += dv;
    }

    // helper method to help detect color 

    bool CheckColor(Color one, Color two, float delta = 0.05f)
    {
        return Math.Abs(one.r - two.r) < delta && Math.Abs(one.g - two.g) < delta && Math.Abs(one.b - two.b) < delta;
    }

    // finds the nearest flag to each respective billion
    GameObject FindNearestFlag()
    {
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");
        GameObject closestFlag = null;
        float dist = float.MaxValue;

        foreach (GameObject flag in flags)
        {
            SpriteRenderer individualSR = flag.GetComponent<SpriteRenderer>();

            // if the color of the sprite renderers don't match, billion should not go towards that flag
            if (!CheckColor(individualSR.color, sr.color))
            {
                Debug.Log("null");
                continue;
            }

            float localDist = Vector2.Distance(transform.position, flag.transform.position);

            if (localDist < dist)
            {
                dist = localDist;
                closestFlag = flag;
            }
        }
        return closestFlag;
    }
}
