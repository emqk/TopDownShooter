using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Create New Weapon")]
public class WeaponData : ScriptableObject
{
    public Projectile projectile;
    public float shootRate;
}
