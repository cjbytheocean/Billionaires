using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{
    public float blasterSpeed = 20f;
    public float blasterLife = 5f; // this will be changed
    
    void Start()
    {
        Destroy(gameObject, blasterLife);
    }

    void Update()
    {
        transform.position += transform.up * blasterSpeed * Time.deltaTime;
    }
}
