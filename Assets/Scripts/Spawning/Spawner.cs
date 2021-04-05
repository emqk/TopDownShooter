using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] float spawnRadius;

    private void Awake()
    {
        SpawnManager.instance.RegisterSpawner(this);
    }

    public void SpawnAI(AI toSpawn) 
    {
        float angle = Random.Range(0.0f, Mathf.PI * 2.0f);
        float offset = Random.Range(0.0f, spawnRadius);
        float x = Mathf.Cos(angle) * offset;
        float z = Mathf.Sin(angle) * offset;

        Vector3 targetPosition = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        Quaternion targetRotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);

        AI aiInstance = Instantiate(toSpawn, targetPosition, targetRotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
