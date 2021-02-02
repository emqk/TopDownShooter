using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float nextWaveTimeSpan = 10;
    [SerializeField] List<SpawnWave> spawnWaves;
    int enemiesToKill = 0;
    int currentWaveIndex = -1;
    float timeToNextWave = 0;
    bool isWaitingForNextWave = true;
    bool spawningEnded = false;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeToNextWave = nextWaveTimeSpan;
    }

    public static void RegisterKill(AI killedAI)
    {
        instance.enemiesToKill -= 1;
    }

    void Update()
    {
        if (spawningEnded)
            return;

        if (isWaitingForNextWave)
        {
            timeToNextWave -= Time.deltaTime;

            if (timeToNextWave <= 0)
            {
                PrepareToNextWave();
                StartNextWave();
            }
        }
        else
        {
            if (enemiesToKill <= 0)
            {
                EndCurrentWave();
            }
        }
    }

    void StartNextWave()
    {
        Debug.Log("Wave started!");
        isWaitingForNextWave = false;

        //Start spawning AI
        SpawnWave currWave = spawnWaves[currentWaveIndex];
        foreach (SpawnData spawnData in currWave.spawnData)
        {
            spawnData.spawner.StartSpawning(spawnData);
        }
    }

    void EndCurrentWave()
    {
        Debug.Log("Wave ended!");

        if (currentWaveIndex + 1 >= spawnWaves.Count)
        {
            Debug.Log("That was the last wave on this map. Should show Victory UI panel or something");
            spawningEnded = true;
        }
        else
        {
            isWaitingForNextWave = true;
        }
    }

    void PrepareToNextWave()
    {
        currentWaveIndex += 1;
        foreach (SpawnData spawnData in spawnWaves[currentWaveIndex].spawnData)
        {
            enemiesToKill += spawnData.amountToSpawn;
        }

        timeToNextWave = nextWaveTimeSpan;
    }
}
