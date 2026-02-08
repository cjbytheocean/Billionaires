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
            Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero);

            if (Physics2D.Raycast(v, Vector2.zero))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    DealDamage();
                }
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
