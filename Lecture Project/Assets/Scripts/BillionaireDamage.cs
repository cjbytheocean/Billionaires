using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class BillionaireDamage : MonoBehaviour
{
    private float lowerScaleAmount = 0.04f;
    public int health = 100;
    public int experiencePoints = 0;

    // I had to type UnityEngine.UI.Image to clear ambiguity
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image experienceBar;

    // When pressing on a billion, decrease the scale of the color while maintaining a white "ring"
    // For the use of OnMouseDown(): https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.OnMouseDown.html
    // On each hit of the billions, increase the hit counter and decrease billion health.
    // When health hits zero, destroy billion.
    public void DealBillionaireDamage(int amount)
    {
        health -= amount;
        healthBar.fillAmount -= amount/100f;

        if (health <= 0)
        {
            DestroyBillionaire();
        }
    }

    // void ChangeHealthBar(int amount)
    // {
    //     healthBar.fillAmount -= amount/100f;
    // }

    void ChangeExperienceBar(int degree)
    {
        
    }

    // Destroys the billionaire.
    void DestroyBillionaire()
    {
        Destroy(gameObject);
    }
}
