using UnityEngine;

public class BillionaireExperience : MonoBehaviour
{
    // Only holds a reference to the experience bar.
    public UnityEngine.UI.Image experienceBar;

    public int experiencePoints = 0;
    public int rank = 0;

    // Awards experience to the billionaire when called, does not
    // hold meaning yet.
    public void AwardExperience(int amount)
    {
        experiencePoints += amount;

        Debug.Log("XP now: " + experiencePoints + " on " + gameObject.name);

        if (experiencePoints % 100 == 0 && experiencePoints != 0)
        {
            rank++;
            Debug.Log("rank changed");
        }
    }
}
