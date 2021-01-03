using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Create New Weapon")]
public class WeaponData : PurchaseData
{
    [Header("--------------------------------- [ WEAPON INFO ] ---------------------------------")]
    [Header("Weapon", order = 1)]
    [SerializeField] Weapon prefab;

    [Header("Projectile")]
    [SerializeField] Projectile projectile;
    [SerializeField] ProjectileData projectileData;

    public Weapon Prefab { get => prefab; }
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
