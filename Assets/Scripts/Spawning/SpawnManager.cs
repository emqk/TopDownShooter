using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<SpawnWave> spawnWaves;
    int currentWave = 0;

    void Start()
    {
        SpawnWave currWave = spawnWaves[currentWave];
        foreach (SpawnData spawnData in currWave.spawnData)
        {
            spawnData.spawner.StartSpawning(spawnData);
        }
    }

    void Update()
    {
        
    }
}
