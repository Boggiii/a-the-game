using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool Paused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuSettingsUI;

    public GameObject mainCamera;
    private CameraController cameraController;

    public GameObject weapon;
    private WeaponController weaponController;

    void Start()
    {
        cameraController = mainCamera.GetComponent<CameraController>();
        weaponController = weapon.GetComponent<WeaponController>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        cameraController.lockMouse();
        cameraController.enabled = true;
        weaponController.enabled = true;
        pauseMenuUI.SetActive(false);
        pauseMenuSettingsUI.SetActive(false);
        Paused = false;
    }

    void Pause()
    {
        cameraController.freeMouse();
        cameraController.enabled = false;
        weaponController.enabled = false;
        pauseMenuUI.SetActive(true);
        Paused = true;
    }

    public void Menu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        GameObject.FindGameObjectWithTag("Music").GetComponent<LobbyMusic>().StopMusic();
        SceneManager.LoadScene("StartMenu");
    }
}
