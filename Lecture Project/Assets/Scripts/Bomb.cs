using UnityEngine;

public class Bomb : MonoBehaviour
{
    private PowerupManager pm;

    public void Init(PowerupManager m)
    {
        pm = m;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        TurretBehavior tb = c.GetComponent<TurretBehavior>();
        if (tb != null)
        {
            pm.BombPickup(tb.billionaireColor);
            Destroy(gameObject);
        }
    }
}
