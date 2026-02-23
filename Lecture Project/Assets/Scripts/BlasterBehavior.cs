using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{
    public float blasterSpeed = 10f;
    public float blasterLife = 5f; // this will be changed
    
    void Start()
    {
        Destroy(gameObject, blasterLife);
    }

    void Update()
    {
        transform.position += -transform.right * blasterSpeed * Time.deltaTime;
    }
}
