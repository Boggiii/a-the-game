using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip fightMusic;
    public AudioClip chillMusic;

    public SpawnEnemies spwnEnemies;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void changeMusic()
    {
        if(spwnEnemies.waveStarted)
        {
            audioSource.clip = fightMusic;
        }
        else
        {
            audioSource.clip = chillMusic;
        }
        audioSource.Play();
    }
}
