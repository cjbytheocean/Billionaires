using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public GameObject blueFlag;
    public GameObject greenFlag;

    public GameObject redFlag;
    public GameObject yellowFlag;

    private List<GameObject> blueFlags = new List<GameObject>();
    private List<GameObject> greenFlags = new List<GameObject>();

    private List<GameObject> redFlags = new List<GameObject>();
    private List<GameObject> yellowFlags = new List<GameObject>();

    private Camera c;
    private GameObject dragging;
    private LineRenderer drawing;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            ClickRed();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ClickYellow();
        }

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

    void ClickRed()
    {
        PickColor(redFlags, redFlag); 
    }

    void ClickYellow()
    {
        PickColor(yellowFlags, yellowFlag);
        
    }

    public void PickColor(List<GameObject> ls, GameObject g)
    {
        Vector3 position = PostBottom(g);

        if (ls.Count < 2)
        {
            GameObject newFlag = Instantiate(g, position, Quaternion.identity);
            ls.Add(newFlag);
        }
        else
        {
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
            Collider2D collide = Physics2D.OverlapPoint(position);
            if (collide != null && CheckIfFlag(collide.gameObject))
            {
                dragging = collide.gameObject; 

                drawing = dragging.AddComponent<LineRenderer>();
                drawing.positionCount = 2;
                drawing.startWidth = 0.05f;
                drawing.endWidth = 0.05f;
                drawing.material = new Material(Shader.Find("Sprites/Default"));
                drawing.startColor = Color.gray;
                drawing.endColor = Color.gray;
            } 
        }

        if (dragging != null && Input.GetMouseButton(0) && drawing != null)
        {
            drawing.SetPosition(0, dragging.transform.position);
            drawing.SetPosition(1, position);
        }

        if (dragging != null && Input.GetMouseButtonUp(0))
        {
            dragging.transform.position = PostBottom(dragging);

            if (drawing != null)
            {
                Destroy(drawing);
            }
           
            dragging = null;
            drawing = null;
        }
    }

    Vector3 PostBottom(GameObject g)
    {
        Vector3 position = c.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        SpriteRenderer s = g.GetComponent<SpriteRenderer>();

        float bottom = s.bounds.extents.y;
        return position + Vector3.up * bottom; 
    }

    private bool CheckIfFlag(GameObject g)
    {
        return blueFlags.Contains(g) || greenFlags.Contains(g) || redFlags.Contains(g) || yellowFlags.Contains(g);
    }
}
