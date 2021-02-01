using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] float spawnRadius;
    SpawnData spawnData;

    public void StartSpawning(SpawnData newSpawnData)
    {
        spawnData = newSpawnData;
        StartCoroutine(SpawnAndWait(spawnData.amountToSpawn, spawnData.spawnInterval));
    }

    IEnumerator SpawnAndWait(int amount, float wait)
    {
        for (int i = 0; i < amount; i++)
        {
            AI aiInstance = Instantiate(spawnData.toSpawn);
            aiInstance.transform.position = new Vector3(
                  transform.position.x + Random.Range(-1.0f, 1.0f) * spawnRadius
                , transform.position.y
                , transform.position.z + Random.Range(-1.0f, 1.0f) * spawnRadius);
            yield return new WaitForSeconds(wait);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
