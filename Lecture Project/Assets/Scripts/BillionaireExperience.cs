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

        if (experiencePoints % 200 == 0 && experiencePoints != 0)
        {
            rank++;
        }
    }
}
