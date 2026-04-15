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

    void Start()
    {
        Debug.Log(gameObject.name + " initial rank = " + rank);
    }

    public void AwardExperience(int amount) { 
        experiencePoints += amount; 
        
        while (experiencePoints >= threshold) 
        { 
            experiencePoints -= threshold;
            rank++; 
            rankText.text = $"{rank}"; 
            threshold = threshold * 2; 
        } 

        experienceBar.fillAmount = ((float)experiencePoints/threshold); 
    }

    public void RefreshRank()
    {
        rankText.text = $"{rank}";
        experienceBar.fillAmount = ((float)experiencePoints/threshold); 
    }
}
