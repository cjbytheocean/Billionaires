using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    private float lowerScaleAmount = 0.04f;
    public Transform dontScaleChild;
    public Transform dontScaleTurret;
    public int health = 1;

    public UnityEngine.UI.Image experienceBar;

    void Start()
    {
        TurretBehavior tb = GetComponent<TurretBehavior>();
        
        int rank = tb.shooterBase.rank;
        health = Mathf.RoundToInt(rank * 2.5f);

        if (health <= 0)
        {
            health = 1;
        }
    }

    public void DealBillionDamage(int amount, BillionaireExperience opp)
    {
        health -= amount;

        if (opp != null)
        {
            experienceBar = opp.experienceBar;
        }

        WhiteRingEffect();

        if (health <= 0)
        {
            DestroyBillion(opp);
        }
    }

    public void WhiteRingEffect()
    {
        Vector3 changedScale = transform.localScale - new Vector3(lowerScaleAmount, lowerScaleAmount, 0); 
        Vector3 startingScale = transform.localScale;
        transform.localScale = changedScale; 
        
        Vector3 accountForFactor = new Vector3(startingScale.x / transform.localScale.x, startingScale.y / transform.localScale.y, 0); 
        dontScaleChild.localScale = Vector3.Scale(dontScaleChild.localScale, accountForFactor); 
        dontScaleTurret.localScale = Vector3.Scale(dontScaleTurret.localScale, accountForFactor);
    }

    void DestroyBillion(BillionaireExperience opp)
    {
        if (opp != null && opp.experienceBar != null)
        {
            opp.AwardExperience(15);
        }
    
        Destroy(gameObject);
    }
}
