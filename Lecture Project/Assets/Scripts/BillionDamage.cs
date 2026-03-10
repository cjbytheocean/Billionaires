using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    private float lowerScaleAmount = 0.04f;
    public Transform dontScaleChild;
    public Transform dontScaleTurret;
    public int health = 100;

    public UnityEngine.UI.Image experienceBar;
    
    // When called, deal the corresponding amount of damage to the billions and
    // handles opponent's experience bar.
    // Calls WhiteRingEffect(), which shows the damage dealt the billion.
    // When health hits zero, destroy billion.
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

    // From old verison of this class, just moved into a method call. Is called
    // when damage is dealt.

    public void WhiteRingEffect()
    {
        Vector3 changedScale = transform.localScale - new Vector3(lowerScaleAmount, lowerScaleAmount, 0); 
        Vector3 startingScale = transform.localScale;
        transform.localScale = changedScale; 
        
        Vector3 accountForFactor = new Vector3(startingScale.x / transform.localScale.x, startingScale.y / transform.localScale.y, 0); 
        dontScaleChild.localScale = Vector3.Scale(dontScaleChild.localScale, accountForFactor); 
        dontScaleTurret.localScale = Vector3.Scale(dontScaleTurret.localScale, accountForFactor);
    }

    // Destroys the billion.
    void DestroyBillion(BillionaireExperience opp)
    {
        if (opp != null && opp.experienceBar != null)
        {
            opp.experienceBar.fillAmount += 0.05f;
        }
        Destroy(gameObject);
    }
}
