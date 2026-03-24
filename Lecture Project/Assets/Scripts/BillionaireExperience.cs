using TMPro;
using UnityEngine;

public class BillionaireExperience : MonoBehaviour
{
    // Only holds a reference to the experience bar.
    public UnityEngine.UI.Image experienceBar;

    public int experiencePoints = 0;
    public int rank = 0;

    public TextMeshProUGUI rankText;

    int threshold = 50;

    // Awards experience to the billionaire when called, fills its
    // experience bar accordingly, and handles rank ups.

    public void AwardExperience(int amount)
    {
        experiencePoints += amount;
        experienceBar.fillAmount += ((float)amount/threshold);

        if (experiencePoints % threshold == 0 && experiencePoints != 0)
        {
            rank++;
            rankText.text = $"{rank}";
            experienceBar.fillAmount -= 1f;
            threshold = threshold * 2;
        }
    }
}
