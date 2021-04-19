using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Create New Weapon")]
public class WeaponData : PurchaseData
{
    [Header("--------------------------------- [ WEAPON INFO ] ---------------------------------")]
    [Header("Projectile", order = 1)]
    [SerializeField] Projectile projectile;

    public Projectile Projectile { get => projectile; }

    [Header("Stats")]
    [SerializeField] float shootRate;
    [SerializeField] float accuracy;
    public float ShootRate { get => shootRate; }
    public float Accuracy { get => accuracy; }

    [Header("AudioVisuals")]
    [SerializeField] AudioClip shootSound;
    public AudioClip ShootSound { get => shootSound; }

    public WeaponData(WeaponData weaponData)
    {
        projectile = weaponData.projectile;

        shootRate = weaponData.shootRate;
        accuracy = weaponData.accuracy;

        shootSound = weaponData.shootSound;
    }

    public void SetShootRate(float newShootRate)
    {
        shootRate = newShootRate;
    }
}
