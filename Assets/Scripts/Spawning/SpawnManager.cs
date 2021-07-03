using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float nextWaveTimeSpan = 5;
    [SerializeField] List<SpawnWave> spawnWaves;
    int enemiesToKill = 0;
    int currentWaveIndex = -1;
    float timeToNextWave = 0;
    bool isWaitingForNextWave = true;
    bool spawningEnded = false;

    List<Spawner> spawners = new List<Spawner>();
    NavMeshTriangulation navMeshTriangulation;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeToNextWave = nextWaveTimeSpan;
        navMeshTriangulation = NavMesh.CalculateTriangulation();
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


        // Handle spawn wave
        SpawnWave currWave = spawnWaves[currentWaveIndex];

        // Start spawning AI
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
                "Enemies defeated!", 2
            );
            NotificationManager.instance.ShowNotification(screenNotification);

            // Spawn collectables
            SpawnWave currWave = spawnWaves[currentWaveIndex];
            foreach (CollectableSpawnWaveData collectableSpawnWaveData in currWave.collectableSpawnData)
            {
                for (int i = 0; i < collectableSpawnWaveData.amountToSpawn; i++)
                {
                    SpawnAtRandomOnNavMesh(collectableSpawnWaveData.toSpawn.gameObject);
                }
            }

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
            //SpawnAtRandomSpawner(spawnData.toSpawn);
            SpawnAtRandomOnNavMesh(spawnData.toSpawn.GetReference().gameObject);
        }
    }

    void SpawnAtRandomOnNavMesh(GameObject toSpawn)
    {
        // Pick the first indice of a random triangle in the nav mesh
        int navMeshTrisMax = Random.Range(0, navMeshTriangulation.indices.Length - 3);

        // Select a random point on it
        Vector3 spawnPoint = Vector3.Lerp(navMeshTriangulation.vertices[navMeshTriangulation.indices[navMeshTrisMax]], navMeshTriangulation.vertices[navMeshTriangulation.indices[navMeshTrisMax + 1]], Random.value);
        Vector3.Lerp(spawnPoint, navMeshTriangulation.vertices[navMeshTriangulation.indices[navMeshTrisMax + 2]], Random.Range(0.0f, 1.0f));

        GameObject instance = Instantiate(toSpawn, spawnPoint, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        Debug.Log("Spawned!");
    }
}
