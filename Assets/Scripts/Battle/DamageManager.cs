using UnityEngine;

public class DamageManager : MonoBehaviour
{
    Player player;

    public static DamageManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = PlayerController.instance.GetControlledPlayer();    
    }

    public void ApplyRadialDamageToPlayer(int damage, Vector3 source, float radius)
    {
        ParticleManager.instance.SpawnRadialDamageVisualizer(source, radius * 2.0f);
        ApplyDamageToObject(damage, player);
    }

    public void ApplyDamageToPlayer(int damage)
    {
        ApplyDamageToObject(damage, player);
    }

    public void ApplyDamageToObject(int damage, IDamageable objToDamage)
    {
        objToDamage.TakeDamage(damage);
    }
}
