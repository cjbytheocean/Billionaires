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
    private GameObject dragging;
    private LineRenderer drawing;


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

    void PickColor(List<GameObject> ls, GameObject g)
    {
        
    }
}
