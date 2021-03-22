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
        Debug.Log("Reward: " + killedAI.GetReward());
        instance.enemiesToKill -= 1;
    }

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }

    public int GetNumberOfWaves()
    {
        return spawnWaves.Count;
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
        //Show notification about starting wave
        ScreenNotificationData screenNotification = new ScreenNotificationData
        (
            $"Wave {currentWaveIndex + 1}!",
            2
        );
        NotificationManager.instance.ShowNotification(screenNotification);

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
        if (currentWaveIndex + 1 >= spawnWaves.Count)
        {
            currentWaveIndex += 1;
            UIManager.instance.ShowEndPanel();
            spawningEnded = true;
        }
        else
        {
            //Show notification about ending wave
            ScreenNotificationData screenNotification = new ScreenNotificationData
            (
                "Enemies defeated!",
                2
            );
            NotificationManager.instance.ShowNotification(screenNotification);

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
