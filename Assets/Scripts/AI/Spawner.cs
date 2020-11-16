using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] AI aiToSpawn;
    [SerializeField] int amountToSpawn;
    [SerializeField] float spawnRadius;

    void Start()
    {
        Spawn(amountToSpawn);
    }

    void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AI aiInstance = Instantiate(aiToSpawn);
            aiInstance.SetTarget(targetToFollow);
            aiInstance.transform.position = new Vector3(
                  transform.position.x + Random.Range(-1.0f, 1.0f) * spawnRadius
                , transform.position.y
                , transform.position.z + Random.Range(-1.0f, 1.0f) * spawnRadius);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
