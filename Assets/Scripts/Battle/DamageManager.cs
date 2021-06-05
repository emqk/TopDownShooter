using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;

    void Awake()
    {
        instance = this;
    }

    public void ApplyDamage(IDamageable objToDamage, int damage)
    {
        objToDamage.TakeDamage(damage);
    }
}
