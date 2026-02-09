using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    public int destroyClicks = 5;
    public int clicks = 0;
    private SpriteRenderer sr;
    Vector3 scale;


    void Awake()
    {
        scale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
    }


    void OnMouseDown()
    {
        DealDamage();
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
