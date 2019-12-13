using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float startHealth;
    public float currentHealth;
    public Slider HealthBar;
    public Text HealthText;
    public Animator anim;
    public GameObject Gun;

    private PlayerMovement playerMovement;
    private float lastArmor;
    public PlayerArmor playerArmor;
    private float Timer = 0f;
    bool Dead;
    private Animator textAnim;
    public Text deadText;

    void Awake()
    {
        textAnim = deadText.GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerArmor = GetComponent<PlayerArmor>();
        currentHealth = startHealth;
        HealthBar.maxValue = startHealth;

        UpdateBars();
    }

    public void TakeDamage(float amount)
    {
        Timer = 0f;
        Debug.Log(Timer);
        if (playerArmor.currentArmor > 0)
        {
            lastArmor = playerArmor.currentArmor;
            playerArmor.currentArmor -= amount;
            if (playerArmor.currentArmor <= 0)
            {
                currentHealth = currentHealth - (amount - lastArmor);
                playerArmor.currentArmor = 0;
            }
        }
        else
        {
            currentHealth -= amount;
        }

        if (currentHealth <= 0 && !Dead)
        {
            playerMovement.enabled = false;
            currentHealth = 0;
            Dead = true;
            Gun.SetActive(false);
            anim.SetTrigger("Die");
            textAnim.Play("WaveText");
            StartCoroutine(EndGame());
        }

        UpdateBars();
    }

    public void UpdateBars()
    {
        playerArmor.armorSlider.value = playerArmor.currentArmor;
        playerArmor.armorText.text = playerArmor.currentArmor.ToString();

        HealthBar.value = currentHealth;
        HealthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= 8f && playerArmor.currentArmor < playerArmor.armorSlider.maxValue && !Dead)
        {
            Timer = 6.5f;
            playerArmor.currentArmor += 5;

            if (playerArmor.currentArmor > playerArmor.armorSlider.maxValue)
            {
                playerArmor.currentArmor = playerArmor.armorSlider.maxValue;
            }
            UpdateBars();
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag("Music").GetComponent<LobbyMusic>().PlayMusic();
        Debug.Log("End");
        SceneManager.LoadScene("Lobby");
    }
}
