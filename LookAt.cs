using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        gameObject.transform.LookAt(cam.transform);
    }
}
