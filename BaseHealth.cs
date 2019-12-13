using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    public int startHealth;
    public int currentHealth;
    public SpawnEnemies spwnEnemies;
    bool Dead;
    private Animator textAnim;
    public Text deadText;

    public TextMesh BaseHealthIndicator;

    void Awake()
    {
        textAnim = deadText.GetComponent<Animator>();
        currentHealth = startHealth;
        BaseHealthIndicator.text = "Health\n" + currentHealth + "/" + startHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        BaseHealthIndicator.text = "Health \n" + currentHealth + "/" + startHealth;
        if (currentHealth <=0 && !Dead)
        {
            Dead = true;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            spwnEnemies.enabled = false;
            textAnim.Play("WaveText");
            StartCoroutine(EndGame());
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
