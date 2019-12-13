using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    /*
        Voidiin ampuminen ei toimi, ei välttämättä tarvitse fixaa sitä, jos kentät ovat luolastossa.
    */

    public Camera playerView; // Main Camera
    public float damage = 10f;
    public float range = 100f;
    public GameObject shotSpawn; // Aseen piippu
    int shootable;

    private AudioSource audioSource;
    float ajastin;
    float EffectTime = 0.1f;
    public float FireRate = 0.15f;
    LineRenderer Shot;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        shootable = LayerMask.GetMask("Shootable");
        Shot = GetComponent<LineRenderer>(); // Ampumiselle effekti, voidaan muuttaa myöhemmin muzzle flashiksi ja impact effectiks.
    }

    void Update()
    {
        ajastin += Time.deltaTime;

        // Haetaan kameran perusteella aseelle osottamis suunta:
        RaycastHit camHit;
        Physics.Raycast(playerView.transform.position, playerView.transform.forward, out camHit, range, shootable);
        transform.LookAt(camHit.point);

        if (Input.GetButton("Fire1") && ajastin >= FireRate && Time.timeScale != 0)
        {
            Shoot();
        }

        if (ajastin >= FireRate * EffectTime)
        {
            Shot.enabled = false;
        }
    }

    void Shoot()
    {
        audioSource.Play();
        ajastin = 0f;

        Shot.enabled = true;
        Shot.SetPosition(0, transform.position);

        // Ampuminen ja se mihin se osuu
        RaycastHit hit;
        if(Physics.Raycast(shotSpawn.transform.position, shotSpawn. transform.forward, out hit, range, shootable))
        {
            Debug.Log(hit.transform.name); // Otetaan myöhemmin pois, ainoastaan testaamista varten tässä

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.takeDmg(damage);
            }
            Shot.SetPosition(1, hit.point);
        }
        else
        {
            Shot.SetPosition(1, shotSpawn.transform.position + shotSpawn.transform.forward * range);
        }
    }
}
