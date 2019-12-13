using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(new Vector3(2f, 3f, 1f) * speed * Time.deltaTime);
    }
}
