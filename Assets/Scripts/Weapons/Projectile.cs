using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask damageLayerMask;
    [SerializeField] ProjectileData defaultProjectileData;
    protected ProjectileData upgradedProjectileData; //Projectile data agter applying upgrades
    Vector3 lastFramePos = new Vector3();
    RaycastHit hit = new RaycastHit();

    public void SetUpgradedProjectileData(ProjectileData newUpgradedProjectileData)
    {
        upgradedProjectileData = newUpgradedProjectileData;
    }

    private void Start()
    {
        lastFramePos = transform.position;
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        transform.position += transform.forward * upgradedProjectileData.MoveSpeed * Time.deltaTime;
        CheckCollision();
        lastFramePos = transform.position;
    }

    public ProjectileData GetDefaultProjectileData()
    {
        return defaultProjectileData;
    }

    public virtual void OnHit(RaycastHit hit)
    {
        IDamageable damageable = hit.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            int damageToGive = Random.Range(upgradedProjectileData.DamageRange.x, upgradedProjectileData.DamageRange.y);
            DamageManager.instance.ApplyDamageToObject(damageToGive, damageable);
        }

        ParticleManager.instance.SpawnHitParticle(hit.point, Quaternion.LookRotation(hit.normal));
    }

    void CheckCollision()
    {
        if (Physics.Linecast(lastFramePos, transform.position, out hit, damageLayerMask))
        {
            OnHit(hit);
            Destroy(gameObject);
        }
    }
}
