using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    public int destroyClicks = 5;
    public int clicks = 0;
    private SpriteRenderer sr;
    Vector3 scale;

    private float lowerScaleAmount = 0.06f;
    public Transform dontScaleChild;


    void Awake()
    {
        scale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
    }


    void OnMouseDown()
    {
        Vector3 changedScale = transform.localScale - new Vector3(lowerScaleAmount, lowerScaleAmount, 0); 
        Vector3 startingScale = transform.localScale;
        transform.localScale = changedScale; 
        
        Vector3 accountForFactor = new Vector3; 

        
        dontScaleChild.localScale = Vector3.Scale(dontScaleChild.localScale, accountForFactor);
    }

    void DealDamage()
    {
        clicks++;
        if (clicks == destroyClicks)
        {
            DestroyBillion();
        }
    }
    void DestroyBillion()
    {
        Destroy(gameObject);
    }
}
