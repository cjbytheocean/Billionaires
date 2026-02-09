using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    private float lowerScaleAmount = 0.06f;
    public Transform dontScaleChild;
    public int destroyClicks = 5;
    public int clicks = 0;
    public int health = 100;

    // When pressing on a billion, decrease the scale of the color while maintaining a white "ring"
    // For the use of OnMouseDown(): https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.OnMouseDown.html
    void OnMouseDown()
    {
        DealDamage();
        Vector3 changedScale = transform.localScale - new Vector3(lowerScaleAmount, lowerScaleAmount, 0); 
        Vector3 startingScale = transform.localScale;
        transform.localScale = changedScale; 
        
        Vector3 accountForFactor = new Vector3(startingScale.x / transform.localScale.x, startingScale.y / transform.localScale.y, 0); 
        dontScaleChild.localScale = Vector3.Scale(dontScaleChild.localScale, accountForFactor);
    }

    // On each click of the billions, increase the click counter
    // Additionally, for now, decrease billion health.
    void DealDamage()
    {
        clicks++;
        health -= 20;
        if (clicks == destroyClicks)
        {
            health = 0;
            DestroyBillion();
        }
    }

    // Destroys the billion.
    void DestroyBillion()
    {
        Destroy(gameObject);
    }
}
