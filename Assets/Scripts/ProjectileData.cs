using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Weapons/Create New Projectile")]
public class ProjectileData : ScriptableObject
{
    public Vector2Int DamageRange { get => damageRange; }
    [SerializeField] Vector2Int damageRange;

    public float MoveSpeed { get => moveSpeed; }
    [SerializeField] float moveSpeed;

    public ProjectileData(ProjectileData other)
    {
        damageRange = other.damageRange;
        moveSpeed = other.moveSpeed;
    }

    public ProjectileData(Vector2Int _damageRange, float _moveSpeed)
    {
        damageRange = _damageRange;
        moveSpeed = _moveSpeed;
    }
}
