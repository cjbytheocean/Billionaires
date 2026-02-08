using UnityEngine;

public class BillionDamage : MonoBehaviour
{
    public GameObject billionPrefab;

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
    
    }
    void DestroyBillion()
    {
        Destroy(gameObject);
    }
}
