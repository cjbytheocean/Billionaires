using System;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BillionMovement : MonoBehaviour
{
    float maxSpeed = 5f;
    float accSpeed = 7f;
    float slowDownWithinRange = 5f;
  
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject flag;

    private GameObject[] allFlags;

    // Rigidbody and SpriteRenderer components, removes gravity so the billions don't immediately fall, then lowers damping to control velocity.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0.005f;
    }

    // I utilized AI to assist with the Vector math. First time doing work like this, so I was unfamiliar with it.
    // Handles the actual movement of the billions, with them accelerating towards the nearest flag, then slowing down on approach.
    void FixedUpdate()
    {
        flag = FindNearestFlag();

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
        s = Vector2.ClampMagnitude(s, accSpeed);
        rb.AddForce(s);
    }

    // Finds the near flag in respective to each billion of the bases respective color.
    GameObject FindNearestFlag()
    {
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");
        GameObject closestFlag = null;
        float dist = float.MaxValue;

        foreach (GameObject flag in flags)
        {
            // Documentation for this line:
            // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Component.TryGetComponent.html

            if (flag.TryGetComponent<SpriteRenderer>(out var individualSR))
            {
                if (CheckColor(individualSR.color, sr.color))
                {
                    float localDist = Vector2.Distance(transform.position, flag.transform.position);

                    if (localDist < dist)
                    {
                        dist = localDist;
                        closestFlag = flag;
                    }  
                }
            }
        }
        return closestFlag;
    }

    // Helper method to check the similarity between two colors.
    bool CheckColor(Color one, Color two, float delta = 0.05f)
    {
        return Mathf.Abs(one.r - two.r) < delta && Mathf.Abs(one.g - two.g) < delta && Mathf.Abs(one.b - two.b) < delta;
    }
}