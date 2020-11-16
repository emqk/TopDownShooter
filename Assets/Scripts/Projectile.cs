using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileData projectileData;
    Vector3 lastFramePos = new Vector3();
    RaycastHit hit = new RaycastHit();

    public void Init(ProjectileData data)
    {
        projectileData = data;
    }

    private void Start()
    {
        lastFramePos = transform.position;
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        transform.position += transform.forward * projectileData.moveSpeed * Time.deltaTime;
        CheckCollision();
        lastFramePos = transform.position;
    }

    void CheckCollision()
    {
        if (Physics.Linecast(lastFramePos, transform.position, out hit))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                int damageToGive = Random.Range(projectileData.damageRange.x, projectileData.damageRange.y);
                damageable.TakeDamage(damageToGive);
            }

            ParticleManager.instance.SpawnParticle(hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(gameObject);
        }
    }
}
