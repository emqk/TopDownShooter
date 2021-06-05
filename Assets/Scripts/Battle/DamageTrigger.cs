using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] LayerMask damageMask;

    private void OnTriggerEnter(Collider other)
    {
        if (ContainsLayer(damageMask, other.gameObject.layer))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                DamageManager.instance.ApplyDamageToObject(damage, damageable);
            }
        }
    }

    bool ContainsLayer(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
