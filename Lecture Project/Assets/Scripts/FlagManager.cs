using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
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
        
    }

    void ClickBlue()
    {
        PickColor(blueFlags, blueFlag);
    }

    void ClickGreen()
    {
        PickColor(greenFlags, greenFlag);
    }

    // Commented out for now, but will utilize these when all four bases are added.
    
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
