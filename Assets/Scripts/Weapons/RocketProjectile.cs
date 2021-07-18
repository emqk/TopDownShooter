using UnityEngine;

public class RocketProjectile : Projectile
{
    float damageRadius = 2.5f;   

    public override void OnHit(RaycastHit hit)
    {
        int damageToGive = Random.Range(upgradedProjectileData.DamageRange.x, upgradedProjectileData.DamageRange.y);
        DamageManager.instance.PerformApplyRadialDamageToAllInRadius(damageToGive, transform.position, damageRadius);
    }
}
