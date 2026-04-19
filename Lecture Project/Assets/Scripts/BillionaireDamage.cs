using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class BillionaireDamage : MonoBehaviour
{
    public int health = 100;
 
    public UnityEngine.UI.Image healthBar;
    
    public void DealBillionaireDamage(int amount)
    {
        health -= amount;
        healthBar.fillAmount -= amount/100f;
        
        if (health <= 0)
        {
            DestroyBillionaire();
        }
    }

    void DestroyBillionaire()
    {
        Destroy(gameObject);
    }
}
