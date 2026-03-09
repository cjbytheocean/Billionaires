using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    private float lowerScaleAmount = 0.04f;
    public Transform dontScaleChild;
    public Transform dontScaleTurret;
    public int destroyHits = 4; 
    public int hits = 0;
    public int health = 100;

    public UnityEngine.UI.Image experienceBar;

    // When pressing on a billion, decrease the scale of the color while maintaining a white "ring"
    // For the use of OnMouseDown(): https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.OnMouseDown.html
    // On each hit of the billions, increase the hit counter and decrease billion health.
    // When health hits zero, destroy billion.
    public void DealBillionDamage(int amount)
    {
        hits++;
        health -= amount;

        WhiteRingEffect();
        
        if (hits == destroyHits)
        {
            health = 0;
            DestroyBillion();
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
    void DestroyBillion()
    {
        if (experienceBar != null)
        {
            experienceBar.fillAmount += 0.05f;
        }
        Destroy(gameObject);
    }
}
