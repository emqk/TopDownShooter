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
    [SerializeField] float accuracy;
    public float ShootRate { get => shootRate; }
    public float HeatPerShot { get => heatPerShot; }
    public float Accuracy { get => accuracy; }

    [Header("AudioVisuals")]
    [SerializeField] AudioClip shootSound;
    public AudioClip ShootSound { get => shootSound; }
}
