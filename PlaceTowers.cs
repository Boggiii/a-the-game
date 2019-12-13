using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTowers : MonoBehaviour
{
    public GameObject tower;
    public LevelController lvlController;
    public float placeRange = 35f;
    int placeable;

    private GameObject currentObject;
    
    void Awake()
    {
        placeable = LayerMask.GetMask("Placeable");
    }

    void Update()
    {
        if (lvlController.towerCount > 0)
        {
            SpawnTower();

            if (currentObject != null)
            {
                MoveCurrentObject();
                ReleaseOnClick();
            }
        }
    }

    private void ReleaseOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lvlController.towerCount--;
            currentObject.GetComponent<TowerAttack>().enabled = true;
            currentObject = null;

            if (lvlController.towerCount <= 0)
            {
                lvlController.SetTowerNotification(false);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentObject);
            currentObject.GetComponent<TowerAttack>().enabled = true;
        }
    }

    private void MoveCurrentObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, placeRange, placeable))
        {
            currentObject.transform.position = hitInfo.point;
            currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void SpawnTower()
    {
        if (Input.GetKeyDown("x"))
        {
            if (currentObject == null)
            {
                currentObject = Instantiate(tower);
                currentObject.GetComponent<TowerAttack>().enabled = false;
            }
        }
    }
}
