using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private float defaultSens = 2;
    public static float rotateSens;
    public Slider sensSlider;
    public Text sensValue;
    public Transform target, player;
    float mouseX, mouseY;

    private bool lockView = false;

    public void Sensitivity(float sens)
    {
        rotateSens = sens;
        sensValue.text = rotateSens.ToString();
        PlayerPrefs.SetFloat("Sens", sens);
        PlayerPrefs.Save();
    }

    void Start()
    {
        QualitySettings.vSyncCount = 0;

        sensValue.text = PlayerPrefs.GetFloat("Sens").ToString();
        sensSlider.value = PlayerPrefs.GetFloat("Sens");


        if (rotateSens == 0)
        {
            rotateSens = defaultSens;
        }
        lockMouse();
    }
    
    void Update()
    {
        if (!lockView)
        {
            CamControl();
        }
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotateSens;
        mouseY -= Input.GetAxis("Mouse Y") * rotateSens;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    public void lockMouse()
    {
        lockView = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void freeMouse()
    {
        lockView = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
