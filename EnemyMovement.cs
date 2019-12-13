using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform Base;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;

    public float range;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Base = GameObject.FindGameObjectWithTag("Base").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void searchForObjects()
    {
        if (nav.enabled == true && Base != null)
        {
            if (Vector3.Distance(player.position, transform.position) <= range && Vector3.Distance(Base.position, transform.position) > 20)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.SetDestination(Base.position);
            }
        }
        else
        {
            return;
        }
    }
    
    void Update()
    {
        if (Base != null)
        {
            searchForObjects();
        }
    }
}
