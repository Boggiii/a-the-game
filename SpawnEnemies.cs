using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public int[] enemyCount;
    public GameObject[] enemy;
}

public class SpawnEnemies : MonoBehaviour
{
    public Wave[] Waves;
    public Transform[] spawnPoints;
    public float spawnTime;
    public float timeBetweenWaves;
    public Text WaveText;

    public CanvasGroup canvasGroup;
    public Text nextWaveTimer;

    private float Timer;
    public bool waveStarted = false;

    private int currentWave;
    private int totalWaves;

    private int waveEnemyCount;
    private int enemiesLeft;

    private Animator anim;
    public Text waveCompleted;

    public List<int> EnemyList;

    public Text CompletionText;

    public MusicController musicController;

    void Start()
    {
        anim = waveCompleted.GetComponent<Animator>();
        Timer = 0;
        currentWave = -1;
        totalWaves = Waves.Length - 1;

        musicController.changeMusic();
        WaveText.text = "Wave " + (currentWave + 1) + " / " + (totalWaves + 1);
    }

    private void Update()
    {
        if (!waveStarted)
        {
            Timer += Time.deltaTime;
            nextWaveTimer.text = Mathf.RoundToInt(timeBetweenWaves - Timer).ToString();

            if(timeBetweenWaves - Timer <= 0 || Input.GetKeyDown("g"))
            {
                NextWave();
            }
        }
    }

    private void NextWave()
    {
        canvasGroup.alpha = 0f;
        waveStarted = true;
        musicController.changeMusic();
        currentWave++;
        waveEnemyCount = 0;
        enemiesLeft = 0;

        WaveText.text = "Wave " + (currentWave + 1) + " / " + (totalWaves + 1);

        for (int iteration = 0; iteration < Waves[currentWave].enemyCount.Length; iteration++)
        {
            waveEnemyCount += Waves[currentWave].enemyCount[iteration];
        }
        ListFromEnemies();
        enemiesLeft = EnemyList.Count;
        StartCoroutine(Spawn());
    }

    private void ListFromEnemies() //Listataan viholliset ja arvotaan niille paikat listaan
    {
        EnemyList.Clear();
        for (int enemyType = 0; enemyType < Waves[currentWave].enemy.Length; enemyType++)
        {
            for (int enemyInt = 0; enemyInt < Waves[currentWave].enemyCount[enemyType]; enemyInt++)
            {
                Debug.Log("Enemy to list ID: " + enemyType);
                EnemyList.Add(enemyType);
            }
        }

        for (int i = 0; i < EnemyList.Count; i++)
        {
            int temp = EnemyList[i];
            int randomIndex = Random.Range(i, EnemyList.Count);
            EnemyList[i] = EnemyList[randomIndex];
            EnemyList[randomIndex] = temp;
        }
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < waveEnemyCount; i++)
        {
            int spawnPoint = Random.Range(0, spawnPoints.Length);

            Debug.Log("Enemy spawned ID: " + EnemyList[i]);
            Instantiate(Waves[currentWave].enemy[EnemyList[i]], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void EnemyDead()
    {
        enemiesLeft--;

        if(enemiesLeft == 0)
        {
            canvasGroup.alpha = 1f;
            Timer = 0;
            waveStarted = false;
            musicController.changeMusic();
            anim.Play("Empty");
            if (currentWave < totalWaves)
            {
                CompletionText.text = "Wave completed!";
            }
            else
            {
                CompletionText.text = "Victory!";
            }
            anim.Play("WaveText");
        }
    }
}
