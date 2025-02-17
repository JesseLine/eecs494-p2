using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaveController : MonoBehaviour
{
    //public List<GameObject> spawnPoints;
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI endWaveText;

    public float enemyIncreaseRatio = 1.5f;
    public float spawnRate = 2f;

    private float spawnTimer;

    public int currentWave = 0;

    private int initialWaveEnemyCount = 5;
    private int enemyCount;
    private int enemiesRemaining;
    private bool waveOver = true;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveText.text = "Wave " + currentWave.ToString();
        endWaveText.text = "Press SPACE to start wave";
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        enemyCount = initialWaveEnemyCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && waveOver)
        {
            Debug.Log("space pressed");
            StartWave();
        }
        if (!waveOver)
        {
            if (spawnTimer <= 0)
            {
                //spawn enemy
                SpawnEnemies();
            }
            else
            {
                spawnTimer -= Time.fixedDeltaTime;
            }

            if(enemiesRemaining <= 0)
            {
                EndWave();
            }
        }
        
    }

    void StartWave()
    {
        waveOver = false;
        //incremet wave number
        initialWaveEnemyCount = (int)(initialWaveEnemyCount * enemyIncreaseRatio);
        enemyCount = initialWaveEnemyCount;
        enemiesRemaining = enemyCount;
        currentWave++;
        waveText.text = "Wave " + currentWave.ToString();
        endWaveText.text = "";
    }

    void SpawnEnemies()
    {
        if(enemyCount > 0)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[index].transform.position, Quaternion.identity);
            enemyCount--;
            spawnTimer = spawnRate;
        }
    }

    void EndWave()
    {
        endWaveText.text = "Wave Ended: Press SPACE to start next wave";
        waveOver = true;
    }
}
