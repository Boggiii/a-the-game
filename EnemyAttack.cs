using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;

    GameObject player;
    PlayerHealth playerHealth;

    GameObject Base;
    BaseHealth baseHealth;

    Enemy enemyHealth;

    bool baseInRange;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        Base = GameObject.FindGameObjectWithTag("Base");
        baseHealth = Base.GetComponent<BaseHealth>();

        enemyHealth = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            baseInRange = true;
        }

        else if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("pelaaja rangella");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            anim.SetBool("Attack", false);
            baseInRange = false;
        }

        else if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Attack", false);
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && baseInRange && enemyHealth.health > 0)
        {
            anim.SetBool("Attack", false);
            AttackBase();
        }

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.health > 0)
        {
            anim.SetBool("Attack", false);
            AttackPlayer();
        }

        if (baseHealth.currentHealth <= 0)
        {
            anim.SetTrigger("BaseDestroyed");
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
    }


    void AttackBase()
    {
        timer = 0f;

        if (baseHealth.currentHealth > 0)
        {
            anim.SetBool("Attack", true);
            baseHealth.TakeDamage(attackDamage);
        }
    }

    void AttackPlayer()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            anim.SetBool("Attack", true);
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
