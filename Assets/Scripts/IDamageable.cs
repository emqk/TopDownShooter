public interface IDamageable
{
    void TakeDamage(int damageAmount);
    void Die();
    bool IsInDamageRadius(float distance, float radius);
}
