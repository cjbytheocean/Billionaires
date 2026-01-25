using System;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    // In my game field, I only have blue and green bases at the moment. I started to write methods that will
    // work with the red and yellow bases, but the code will be commented out until it is used.
    public GameObject blueFlag;
    public GameObject greenFlag;

    // public GameObject redFlag;
    // public GameObject yellowFlag;

    private List<GameObject> blueFlags = new List<GameObject>();
    private List<GameObject> greenFlags = new List<GameObject>();

    // private List<GameObject> redFlags = new List<GameObject>();
    // private List<GameObject> yellowFlags = new List<GameObject>();

    private Camera c;
    private GameObject dragging; // For dragging the flags.
    private LineRenderer drawing; // For drawing the line from point A to point B.


    // Converts screen coordinates to handle flag placement.
    void Start()
    {
        c = Camera.main; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ClickBlue();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ClickGreen();
        }

        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     ClickRed();
        // }

        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     ClickYellow();
        // }

        FlagDragging();
        
    }

    void ClickBlue()
    {
        PickColor(blueFlags, blueFlag);
    }

    void ClickGreen()
    {
        PickColor(greenFlags, greenFlag);
    }

    // void ClickRed()
    // {
    //     PickColor(redFlags, redFlag); 
    // }

    // void ClickYellow()
    // {
    //     PickColor(yellowFlags, yellowFlag);
        
    // }

    public void PickColor(List<GameObject> ls, GameObject g)
    {
    
        // Uses current mouse position to place flags, calls helper method to handle placement.
        Vector3 position = PostBottom(g);

        // Ensures only two flags per unique color can be added.
        if (ls.Count < 2)
        {
            GameObject newFlag = Instantiate(g, position, Quaternion.identity);
            ls.Add(newFlag);
        }
        else
        {
            // Finds nearest flag, moves that one.
            GameObject nearestFlag = ls[0];
            float dist = Vector3.Distance(position, nearestFlag.transform.position);

            for (int i = 1; i < ls.Count; i++)
            {
                float dist2 = Vector3.Distance(position, ls[i].transform.position);
                if (dist2 < dist)
                {
                    dist = dist2;
                    nearestFlag = ls[i];
                } 
            }
            nearestFlag.transform.position = position;
        } 
    }

    public void FlagDragging()
    {
        Vector3 position = c.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    // Helper method to reference the bottom of the flag post.

    Vector3 PostBottom(GameObject g)
    {
        Vector3 position = c.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        SpriteRenderer s = g.GetComponent<SpriteRenderer>();

        // Looked this up to help with bottom offset. Half of the vertical height.
        float bottom = s.bounds.extents.y;
        return position + Vector3.up * bottom; 
    }
}
