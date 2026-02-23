using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    private float lowerScaleAmount = 0.04f;
    public Transform dontScaleChild;
    public int destroyHits = 4; 
    public int hits = 0;
    public int health = 100;

    // When pressing on a billion, decrease the scale of the color while maintaining a white "ring"
    // For the use of OnMouseDown(): https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.OnMouseDown.html

        // Vector3 changedScale = transform.localScale - new Vector3(lowerScaleAmount, lowerScaleAmount, 0); 
        // Vector3 startingScale = transform.localScale;
        // transform.localScale = changedScale; 
        
        // Vector3 accountForFactor = new Vector3(startingScale.x / transform.localScale.x, startingScale.y / transform.localScale.y, 0); 
        // dontScaleChild.localScale = Vector3.Scale(dontScaleChild.localScale, accountForFactor);
    
    // On each click of the billions, increase the click counter
    // Additionally, for now, decrease billion health.
    public void DealDamage()
    {
        hits++;
        health -= 25;

        WhiteRingEffect();
        
        if (hits == destroyHits)
        {
            health = 0;
            DestroyBillion();
        }
    }

    // From old verison of this class, just moved into a method call

    public void WhiteRingEffect()
    {
        Vector3 changedScale = transform.localScale - new Vector3(lowerScaleAmount, lowerScaleAmount, 0); 
        Vector3 startingScale = transform.localScale;
        transform.localScale = changedScale; 
        
        Vector3 accountForFactor = new Vector3(startingScale.x / transform.localScale.x, startingScale.y / transform.localScale.y, 0); 
        dontScaleChild.localScale = Vector3.Scale(dontScaleChild.localScale, accountForFactor); 
    }

    // Destroys the billion.
    void DestroyBillion()
    {
        Destroy(gameObject);
    }
}
