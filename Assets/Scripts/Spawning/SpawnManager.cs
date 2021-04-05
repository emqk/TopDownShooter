using System.Collections;
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

    List<Spawner> spawners = new List<Spawner>();

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeToNextWave = nextWaveTimeSpan;
    }

    public void RegisterSpawner(Spawner spawner)
    {
        if (spawner == null)
        {
            Debug.Log("Can't register spawner - spawner is null!");
            return;
        }

        spawners.Add(spawner);
    }

    public static void RegisterKill(AI killedAI)
    {
        BattleManager.instance.AddToReward(killedAI.GetReward());
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
            StartSpawning(spawnData);
        }
    }

    void EndCurrentWave()
    {
        if (currentWaveIndex + 1 >= spawnWaves.Count)
        {
            currentWaveIndex += 1;
            BattleManager.instance.EndBattle();
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

    public void StartSpawning(SpawnData spawnData)
    {
        StartCoroutine(SpawnAndWait(spawnData));
    }

    IEnumerator SpawnAndWait(SpawnData spawnData)
    {
        for (int i = 0; i < spawnData.amountToSpawn; i++)
        {
            yield return new WaitForSeconds(spawnData.spawnInterval);
            SpawnAtRandomSpawner(spawnData.toSpawn);
        }
    }

    void SpawnAtRandomSpawner(AI toSpawn)
    {
        Spawner targetSpawner = GetRandomSpawner();
        targetSpawner.SpawnAI(toSpawn);
    }

    Spawner GetRandomSpawner()
    {
        return spawners[Random.Range(0, spawners.Count)];
    }
}
