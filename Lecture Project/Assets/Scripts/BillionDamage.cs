using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    public GameObject billionPrefab;
    private int destroyClicks = 4;
    private int clicks = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit))
            {
                if (hit.collider.gameObject == billionPrefab)
                {
                    DealDamage();
                }
            }
        }
    }

    void DealDamage()
    {
        clicks++


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
