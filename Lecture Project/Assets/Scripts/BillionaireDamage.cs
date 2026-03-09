using UnityEngine;

public class BillionaireDamage : MonoBehaviour
{
    private float lowerScaleAmount = 0.04f;
    public int health = 100;
    public int experiencePoints = 0;

    // When pressing on a billion, decrease the scale of the color while maintaining a white "ring"
    // For the use of OnMouseDown(): https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.OnMouseDown.html
    // On each hit of the billions, increase the hit counter and decrease billion health.
    // When health hits zero, destroy billion.
    public void DealBillionaireDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            DestroyBillionaire();
        }
    }

    void ChangeHealthBar(int degree)
    {
     
    }

    void ChangeExperienceBar(int degree)
    {
        
    }

    // Destroys the billionaire.
    void DestroyBillionaire()
    {
        Destroy(gameObject);
    }
}
