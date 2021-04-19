using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] List<Transform> shootSources;
    [SerializeField] WeaponData weaponData;
    UpgradeKitData upgradeData;

    bool isOverheated = false;
    float timeToShoot;
    AudioSource audioSource;

    public UpgradeKitData UpgradeData { get => upgradeData; }

    public void Init(UpgradeKitData _upgradeData)
    {
        WeaponData weaponDataCopy = new WeaponData(weaponData);
        weaponData = weaponDataCopy;

        upgradeData = _upgradeData;

        UpgradeDataInstance shootRateUpgrade = UpgradeData.GetUpgradeDataInstanceByType(UpgradeType.Rate);
        float shootRateUpgradeValue = weaponData != null && shootRateUpgrade.GetCurrentPowerCostPair() != null ? shootRateUpgrade.GetCurrentPowerCostPair().power : 1;
        float newShootRate = weaponData.ShootRate * shootRateUpgradeValue;
        weaponData.SetShootRate(newShootRate);
    }

    void Awake()
    {
        timeToShoot = weaponData.ShootRate;
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateMe()
    {
        timeToShoot -= Time.deltaTime;
    }

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public bool Shoot()
    {
        if (timeToShoot <= 0 && !isOverheated)
        {
            ShootFromAllSourcesNoCheck();

            timeToShoot = weaponData.ShootRate;
            audioSource.PlayOneShot(weaponData.ShootSound);
            return true;
        }

        return false;
    }

    void ShootFromAllSourcesNoCheck()
    {
        float weaponAccuracy = weaponData.Accuracy;

        foreach (Transform source in shootSources)
        {
            Vector3 projectileDirection = Quaternion.Euler(0, Random.Range(-weaponAccuracy, weaponAccuracy), 0) * source.forward;
            Quaternion projectileRotation = Quaternion.LookRotation(projectileDirection);

            Projectile projectileInstance = Instantiate(weaponData.Projectile, source.position, projectileRotation);
            ProjectileData projectileData = new ProjectileData(projectileInstance.GetDefaultProjectileData()); // Make copy of default projectile data
            if (UpgradeData != null)
            {
                UpgradeDataInstance damageUpgrade = UpgradeData.GetUpgradeDataInstanceByType(UpgradeType.Damage);
                float damageUpgradeValue = damageUpgrade != null && damageUpgrade.GetCurrentPowerCostPair() != null ? damageUpgrade.GetCurrentPowerCostPair().power : 1;

                UpgradeDataInstance moveSpeedUpgrade = UpgradeData.GetUpgradeDataInstanceByType(UpgradeType.Speed);
                float moveSpeedUpgradeValue = moveSpeedUpgrade != null && moveSpeedUpgrade.GetCurrentPowerCostPair() != null ? moveSpeedUpgrade.GetCurrentPowerCostPair().power : 1;

                projectileData = new ProjectileData(
                    new Vector2Int((int)(projectileData.DamageRange.x * damageUpgradeValue), (int)(projectileData.DamageRange.y * damageUpgradeValue))
                    , projectileData.MoveSpeed * moveSpeedUpgradeValue);
            }
            projectileInstance.SetUpgradedProjectileData(projectileData);
        }
      
    }
}
