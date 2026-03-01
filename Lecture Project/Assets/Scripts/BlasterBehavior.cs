using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{
    public float blasterSpeed = 10f;
    public float blasterLife = 4f;
    public float blasterMaxDistance = 4f;

    public Color blasterColor;
    public bool billionaireBlaster;
    Vector3 blasterSpawnPosition;
    
    void Start()
    {
        blasterSpawnPosition = transform.position;
        Destroy(gameObject, blasterLife);
    }

    // Handles the position of an object. Movement is linear.
    void Update()
    {
        transform.position += -transform.right * blasterSpeed * Time.deltaTime;

        float blasterTraveled = Vector3.Distance(blasterSpawnPosition, transform.position);
        if (blasterTraveled >= blasterMaxDistance)
        {
            Destroy(gameObject);
        }
    }

    // If the blaster touches a border, destroy the blaster.
    // If the blaster touches an opponent, deal damage to the opponent, and destroy the blaster.

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Border"))
        {
            Destroy(gameObject);
            return;
        }

        BillionDamage bd = c.GetComponent<BillionDamage>();

        if (bd != null)
        {
            TurretBehavior thatTB = c.GetComponent<TurretBehavior>();
    
            if (bd != null && thatTB!= null)
            {
                if (thatTB.billionaireColor != blasterColor)
                {
                    bd.DealDamage(25);
                    Destroy(gameObject);
                } 
            }
        }
    }
}
