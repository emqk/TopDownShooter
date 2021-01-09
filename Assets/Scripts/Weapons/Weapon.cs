using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootSource;
    [SerializeField] WeaponData weaponData;
    UpgradeKitData upgradeData;

    WeaponHeatImage weaponHeatImage;

    float currentHeat = 0;
    const float overheatValue = 5;
    bool isOverheated = false;
    float timeToShoot;
    AudioSource audioSource;

    public UpgradeKitData UpgradeData { get => upgradeData; }

    public void Init(UpgradeKitData _upgradeData)
    {
        upgradeData = _upgradeData;
    }

    void Start()
    {
        timeToShoot = weaponData.ShootRate;
        audioSource = GetComponent<AudioSource>();
        weaponHeatImage = UIManager.instance.GetWeaponHeatImage();
    }

    public void UpdateMe()
    {
        timeToShoot -= Time.deltaTime;
        UpdateHeat();
    }

    void UpdateHeat()
    {
        currentHeat -= Time.deltaTime;
        if (currentHeat < 0)
        {
            currentHeat = 0;
            isOverheated = false;
        }
        else if (currentHeat >= overheatValue)
            isOverheated = true;
    }

    public void UpdateWeaponHeatImage()
    {
        weaponHeatImage.Refresh(currentHeat / overheatValue);
    }

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public void Shoot()
    {
        if (timeToShoot <= 0 && !isOverheated)
        {
            Projectile projectileInstance = Instantiate(weaponData.Projectile, shootSource.transform.position, shootSource.rotation);
            ProjectileData projectileData = weaponData.ProjectileData;
            if (UpgradeData != null)
            {
                UpgradeData damageUpgrade = UpgradeData.GetUpgradeDataByType(UpgradeType.Damage);
                float damageUpgradeValue = damageUpgrade != null ? damageUpgrade.GetCurrentPowerCostPair().power : 1;

                UpgradeData moveSpeedUpgrade = UpgradeData.GetUpgradeDataByType(UpgradeType.Speed);
                float moveSpeedUpgradeValue = moveSpeedUpgrade != null ? moveSpeedUpgrade.GetCurrentPowerCostPair().power : 1;

                projectileData = new ProjectileData(
                    new Vector2Int((int)(projectileData.DamageRange.x * damageUpgradeValue), (int)(projectileData.DamageRange.y * damageUpgradeValue))
                    , projectileData.MoveSpeed * moveSpeedUpgradeValue);
            }
            projectileInstance.Init(projectileData);

            timeToShoot = weaponData.ShootRate;
            currentHeat += weaponData.HeatPerShot;
            audioSource.PlayOneShot(weaponData.ShootSound);
            return;
        }
    }
}
