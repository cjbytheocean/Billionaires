using System;
using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{
    public float blasterSpeed = 10f;
    public float blasterLife = 4f;
    public float blasterMaxDistance = 4f;
    public Color blasterColor;
    public bool isBillionaireBlaster;
    Vector3 blasterSpawnPosition;

    public BillionaireExperience opp;

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
        
        if (c.CompareTag("Border"))
        {
            Destroy(gameObject);
            return;
        }

        BillionDamage bd = c.GetComponent<BillionDamage>();
        BillionaireDamage bnd = c.GetComponent<BillionaireDamage>();

        if (bd != null)
        {
            TurretBehavior thatTB = c.GetComponent<TurretBehavior>();
    
            if (bd != null && thatTB != null)
            {
                if (thatTB.billionaireColor != blasterColor)
                {
                    TurretBehavior tb = GetComponentInParent<TurretBehavior>();

                    int damage = 1;

                    if (tb != null && tb.shooterBase != null)
                    {
                        if (tb.shooterBase.rank == 0)
                        {
                            damage = 1;
                        }
                        else
                        {
                            damage = Mathf.RoundToInt(tb.shooterBase.rank / 2f);
                        }
                    }

                    bd.DealBillionDamage(damage, opp);
                    Destroy(gameObject);
                } 
            }
        }
        else if (bnd != null)
        {
            TurretBehavior thatTB = c.GetComponent<TurretBehavior>();
            if (bnd != null && thatTB != null)
            {
                if (thatTB.billionaireColor != blasterColor)
                {
                    bnd.DealBillionaireDamage(1);
                }
                Destroy(gameObject);
            }
        }
    }
}
