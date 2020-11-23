using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] AI aiToSpawn;
    [SerializeField] int amountToSpawn;
    [SerializeField] float spawnRadius;

    void Start()
    {
        StartCoroutine(SpawnAndWait(amountToSpawn, 0.1f));
    }

    IEnumerator SpawnAndWait(int amount, float wait)
    {
        for (int i = 0; i < amount; i++)
        {
            AI aiInstance = Instantiate(aiToSpawn);
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
