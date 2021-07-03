using System.Collections.Generic;

[System.Serializable]
public struct SpawnData
{
    public AIReferenceProxy toSpawn;
    public int amountToSpawn;
    public float spawnInterval;
}

[System.Serializable]
public struct SpawnWave
{
    public List<SpawnData> spawnData;
    public List<CollectableSpawnWaveData> collectableSpawnData;
}
