using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Projectile")]
    public Projectile projectile;
    public ProjectileData projectileData;

    [Header("Stats")]
    public float shootRate;

    [Header("AudioVisuals")]
    public AudioClip shootSound;
}
