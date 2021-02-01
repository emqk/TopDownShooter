using System.Collections.Generic;

[System.Serializable]
public struct SpawnData
{
    public Spawner spawner;
    public int amountToSpawn;
    public AI toSpawn;
    public float spawnInterval;
}

[System.Serializable]
public struct SpawnWave
{
    public List<SpawnData> spawnData;
}
