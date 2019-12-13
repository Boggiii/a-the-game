using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public Image LevelUpUI;

    public PlayerHealth playerHealth;
    public Bullet towerDmg;
    public WeaponController weaponController;
    public CameraController camController;
    public LevelController lvlController;

    public Text HealthUI;
    public Text ArmorUI;
    public Text WeaponUI;
    public Text TowerDmgUI;
    public Text UpgradePoints;
    public Text upgradeHelp;

    private AudioSource audioSource;
    public AudioClip upgradeSuccess;
    public AudioClip upgradeFailed;

    public CanvasGroup canvasGroup;

    bool showMenu = false;

    private void Start()
    {
        towerDmg.damage = towerDmg.startingDamage;
        audioSource = GetComponent<AudioSource>();

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        UpdateMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if(showMenu)
            {
                if (lvlController.upgradePoints > 0)
                {
                    upgradeHelp.text = "Press 'Q' to access Upgrade menu!\nYou have: " + lvlController.upgradePoints + " Upgrade points";
                }
                camController.lockMouse();
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
                showMenu = false;
            }
            else
            {
                upgradeHelp.text = "";
                camController.freeMouse();
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                UpdateMenu();
                showMenu = true;
            }
        }
    }

    public void UpgradeHealth()
    {
        if (lvlController.upgradePoints > 0)
        {
            audioSource.clip = upgradeSuccess;
            int upgrade = Mathf.RoundToInt(playerHealth.HealthBar.maxValue * 0.15f);
            playerHealth.HealthBar.maxValue += upgrade;
            playerHealth.currentHealth += upgrade;
            lvlController.upgradePoints--;
            UpdateMenu();
            playerHealth.UpdateBars();
        }
        else
        {
            audioSource.clip = upgradeFailed;
        }
        audioSource.Play();
    }

    public void UpgradeArmor()
    {
        if (lvlController.upgradePoints > 0)
        {
            audioSource.clip = upgradeSuccess;
            int upgrade = Mathf.RoundToInt(playerHealth.playerArmor.armorSlider.maxValue * 0.15f);
            playerHealth.playerArmor.armorSlider.maxValue += upgrade;
            playerHealth.playerArmor.currentArmor += upgrade;
            lvlController.upgradePoints--;
            UpdateMenu();
            playerHealth.UpdateBars();
        }
        else
        {
            audioSource.clip = upgradeFailed;
        }
        audioSource.Play();
    }

    public void UpgradeWeaponDmg()
    {
        if (lvlController.upgradePoints > 0)
        {
            audioSource.clip = upgradeSuccess;
            weaponController.damage = Mathf.RoundToInt(weaponController.damage * 1.2f);
            lvlController.upgradePoints--;
            UpdateMenu();
        }
        else
        {
            audioSource.clip = upgradeFailed;
        }
        audioSource.Play();
    }

    public void UpgradeTowerDmg()
    {
        if (lvlController.upgradePoints > 0)
        {
            audioSource.clip = upgradeSuccess;
            towerDmg.damage = Mathf.RoundToInt(towerDmg.damage * 1.2f);
            lvlController.upgradePoints--;
            UpdateMenu();
        }
        else
        {
            audioSource.clip = upgradeFailed;
        }
        audioSource.Play();
    }

    public void UpdateMenu()
    {
        HealthUI.text = playerHealth.HealthBar.maxValue.ToString();
        ArmorUI.text = playerHealth.playerArmor.armorSlider.maxValue.ToString();
        WeaponUI.text = weaponController.damage.ToString();
        TowerDmgUI.text = towerDmg.damage.ToString();
        UpgradePoints.text = "Upgrade points: " + lvlController.upgradePoints.ToString();
        Debug.Log("Damage: " + towerDmg.damage);
    }
}
