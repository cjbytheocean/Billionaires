using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{
    public float blasterSpeed = 10f;
    public float blasterLife = 4f;
    public float blasterMaxDistance = 4f;
    Vector3 blasterSpawnPosition;
    
    void Start()
    {
        blasterSpawnPosition = transform.position;
        Destroy(gameObject, blasterLife);
    }

    void Update()
    {
        transform.position += -transform.right * blasterSpeed * Time.deltaTime;

        float blasterTraveled = Vector3.Distance(blasterSpawnPosition, transform.position);
        if (blasterTraveled >= blasterMaxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        BillionDamage bd = c.GetComponent<BillionDamage>();

        if (bd != null)
        {
            TurretBehavior thisTB = GetComponent<TurretBehavior>();
            TurretBehavior thatTB = c.GetComponent<TurretBehavior>();
        }


    }


}
