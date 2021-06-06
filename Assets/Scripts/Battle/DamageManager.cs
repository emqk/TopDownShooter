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

    public bool PerformApplyRadialDamageToPlayer(int damage, Vector3 source, float radius)
    {
        // Show radial damage visualization
        ParticleManager.instance.SpawnRadialDamageVisualizer(source, radius * 2.0f);
       
        // Damage player if in range
        float distToPlayer = Vector3.Distance(source, player.transform.position);
        if (player.IsInDamageRadius(distToPlayer, radius))
        {
            ApplyDamageToPlayer(damage);
            return true;
        }

        return false;
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
