using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 35f;
    public Animator anim;
    public GameObject expOrb;
    public int dropCount;
    GameObject gameController;
    SpawnEnemies spawnEnemies;

    private AudioSource audioSource;

    bool Dead;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        spawnEnemies = gameController.GetComponent<SpawnEnemies>();
    }

    public void takeDmg (float amount)
    {
        health -= amount;
        if(health <= 0f && !Dead)
        {
            Die();
        }
    }

    void Die()
    {
        Dead = true;
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        spawnEnemies.EnemyDead();
        DropLoot();

        anim.SetTrigger("Die");
        audioSource.Play();
        Destroy(gameObject, 2f);
    }

    void DropLoot()
    {
        for(int i = 0; i <= dropCount; i++)
        { 
        Instantiate(expOrb, new Vector3(transform.position.x + Random.Range(-1,1),
            transform.position.y + 0.5f, transform.position.z + Random.Range(-1,1)), Quaternion.identity);
        }
    }
}
