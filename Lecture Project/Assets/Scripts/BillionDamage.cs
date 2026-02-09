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
        //sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapPoint(mousePos);

            if (col != null && col.gameObject == gameObject)
            {
                DealDamage();
            }
        }
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
