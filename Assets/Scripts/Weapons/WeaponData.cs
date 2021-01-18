using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Create New Weapon")]
public class WeaponData : PurchaseData
{
    [Header("--------------------------------- [ WEAPON INFO ] ---------------------------------")]
    [Header("Projectile", order = 1)]
    [SerializeField] Projectile projectile;
    [SerializeField] ProjectileData projectileData;

    public Projectile Projectile { get => projectile; }
    public ProjectileData ProjectileData { get => projectileData; }

    [Header("Stats")]
    [SerializeField] float shootRate;
    [SerializeField] float heatPerShot;
    public float ShootRate { get => shootRate; }
    public float HeatPerShot { get => heatPerShot; }

    [Header("AudioVisuals")]
    [SerializeField] AudioClip shootSound;
    public AudioClip ShootSound { get => shootSound; }
}
