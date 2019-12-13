using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float minModifier = 7;
    public float maxModifier = 11;

    Transform target;
    Animator anim;
    Vector3 Nopeus = Vector3.zero;
    bool Follow = false;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    public void startFollow()
    {
        anim.SetTrigger("PlayerInRange");
        Follow = true;
    }

    void Update()
    {
        if(Follow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref Nopeus, Time.deltaTime * Random.Range(minModifier, maxModifier));
        }
    }
}
