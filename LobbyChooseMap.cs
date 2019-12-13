using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyChooseMap : MonoBehaviour
{
    float startTime = 5f;
    public float currentTime = 0f;

    public Text countdownText;
    public Text mapName;
    bool loadMap = false;
    bool map1 = false;
    bool map2 = false;

    void Start()
    {
        currentTime = startTime;
        mapName.enabled = false;
        countdownText.enabled = false;
    }
    
    void Update()
    {
        if (loadMap)
        {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = "Starting in " + currentTime.ToString("0.0");
            if (currentTime <= 0)
            {
                if (map1)
                {
                    SceneManager.LoadScene("Cave 1");
                    GameObject.FindGameObjectWithTag("Music").GetComponent<LobbyMusic>().StopMusic();
                }
                else if (map2)
                {
                    SceneManager.LoadScene("Cave 2");
                    GameObject.FindGameObjectWithTag("Music").GetComponent<LobbyMusic>().StopMusic();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cave 1"))
        {
            map1 = true;
            loadMap = true;
            mapName.enabled = true;
            countdownText.enabled = true;
            mapName.text = "Cave 1";
        }

        if (other.gameObject.CompareTag("Cave 2"))
        {
            map2 = true;
            loadMap = true;
            mapName.enabled = true;
            countdownText.enabled = true;
            mapName.text = "Cave 2";
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentTime = startTime;
        map1 = false;
        map2 = false;
        loadMap = false;
        mapName.enabled = false;
        countdownText.enabled = false;
    }
}
