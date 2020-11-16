using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Weapons/Create New Projectile")]
public class ProjectileData : ScriptableObject
{
    public Vector2Int damageRange;
    public float moveSpeed;
}
