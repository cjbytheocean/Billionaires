using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class BillionaireDamage : MonoBehaviour
{
    public int health = 100;
    public int experiencePoints = 0;
    public int rank = 0;

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

    // Awards experience to the billionaire when called, does not
    // hold meaning yet.
    public void AwardExperience(int amount)
    {
        experiencePoints += amount;
        Debug.Log("experience points added");
        if (experiencePoints % 100 == 0 && rank != 0)
        {
            rank++;
            Debug.Log("rank changed");
        }
    }

    // Destroys the billionaire.
    void DestroyBillionaire()
    {
        Destroy(gameObject);
    }
}
