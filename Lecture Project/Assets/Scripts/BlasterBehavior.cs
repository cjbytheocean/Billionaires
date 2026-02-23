using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{
    public float blasterSpeed = 20f;
    public float blasterLife = 5f; // this will be changed
    
    public GameObject blaster;
    void Start()
    {
        Destroy(gameObject, blasterLife);
    }

    void Update()
    {
     
    }
}
