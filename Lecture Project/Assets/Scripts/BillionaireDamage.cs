using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class BillionaireDamage : MonoBehaviour
{
    public int health = 100;
 
    // I had to type UnityEngine.UI.Image to clear ambiguity
    public UnityEngine.UI.Image healthBar;
    
    // Called when an opponent's blaster hits the billionaire. Deducts health by the
    // parameter value when called, and the radial health decreases by a corresponding
    // degree. When health hits 0, destroy the billionaire.
    public void DealBillionaireDamage(int amount)
    {
        health -= amount;
        healthBar.fillAmount -= amount/100f;
        
        if (health <= 0)
        {
            DestroyBillionaire();
        }
    }

    // Destroys the billionaire.
    void DestroyBillionaire()
    {
        Destroy(gameObject);
    }
}
