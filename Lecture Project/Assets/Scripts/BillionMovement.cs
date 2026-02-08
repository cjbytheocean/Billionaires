using UnityEngine;
using UnityEngine.UIElements;

public class BillionMovement : MonoBehaviour
{
    float maxSpeed = 5f;
    float accSpeed = 7f;
    float slowSpeed = 1.5f;
  
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0;
    }


    void FixedUpdate()
    {
        GameObject moveHere = FindNearestFlag();


        
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
            if (individualSR.color != sr.color)
            {
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
