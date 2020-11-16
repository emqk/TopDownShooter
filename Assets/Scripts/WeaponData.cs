using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Projectile")]
    [SerializeField] Projectile projectile;
    [SerializeField] ProjectileData projectileData;
    public Projectile Projectile { get => projectile; }
    public ProjectileData ProjectileData { get => projectileData; }

    [Header("Stats")]
    [SerializeField] float shootRate;
    public float ShootRate { get => shootRate; }

    [Header("AudioVisuals")]
    [SerializeField] AudioClip shootSound;
    public AudioClip ShootSound { get => shootSound; }

}
