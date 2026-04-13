using UnityEngine;

public class Bomb : MonoBehaviour
{
    PowerupManager pm;

    public void Init(PowerupManager m)
    {
        pm = m;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        TurretBehavior tb = c.GetComponent<TurretBehavior>();
        if (tb != null)
        {
            Destroy(gameObject);
            
        }
    }
}
