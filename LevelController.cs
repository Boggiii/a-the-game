using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Slider xpSlider; //Oikean alakulman slideri
    public Text LevelText; //Level:
    public Text CurrentXP; //0
    public Text NeededXP; //100
    public Image towerBackground;
    public RawImage towerImage;
    public Text towerText;
    public Text upgradeHelp;
        
    public int startingLevel;
    public int towerCount = 0; //Towerejen määrä alkuun

    private float Xp = 0;
    private int Level = 1;
    public int upgradePoints = 0;

    // Lasku level uppia varten: xpSlider.maxValue = Mathf.Pow(2, Level) * 10;

    private void Start()
    {
        Level = startingLevel;
        xpSlider.maxValue = Mathf.Pow(2, Level) * 10;
        upgradeHelp.text = "";
        LevelText.text = "Level: " + Level;
        CurrentXP.text = Xp.ToString();
        NeededXP.text = xpSlider.maxValue.ToString();
        if (towerCount == 0)
        {
            SetTowerNotification(false);
        }
    }

    public void addXpToSlider(float amount) // Callataan LootCollector.cs scriptistä
    {
        Xp += amount;
        if (Xp >= xpSlider.maxValue) // Level up
        {
            Level++;
            upgradePoints++;
            if (upgradePoints > 0)
            {
                upgradeHelp.text = "Press 'Q' to access Upgrade menu!\nYou have: " + upgradePoints + " Upgrade points";
            }
            Xp = 0 + Xp - xpSlider.maxValue; //Ylimääräinen xp asetetaan seuraavaan leveliin
            xpSlider.maxValue = Mathf.Pow(2, Level) * 10;
            LevelText.text = "Level: " + Level;
            NeededXP.text = xpSlider.maxValue.ToString();

            if(Level % 3 == 0)
            {
                towerCount++;
                SetTowerNotification(true);
                Debug.Log(towerCount);
            }
        }
        CurrentXP.text = Xp.ToString();
        xpSlider.value = Xp;
    }

    public void SetTowerNotification(bool isEnabled)
    {
        towerBackground.enabled = isEnabled;
        towerImage.enabled = isEnabled;
        towerText.enabled = isEnabled;
    }
}