using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem blowUpParticle;

    public static ParticleManager instance;

    void Awake()
    {
        instance = this;
    }

    public void SpawnHitParticle(Vector3 position, Quaternion rotation)
    {
        SpawnParticleDefault(hitParticle, position, rotation);
    }

    public void SpawnBlowUpParticle(Vector3 position, Quaternion rotation)
    {
        SpawnParticleDefault(blowUpParticle, position, rotation);
    }

    void SpawnParticleDefault(ParticleSystem particlePrefab, Vector3 position, Quaternion rotation)
    {
        ParticleSystem instance = Instantiate(particlePrefab, position, rotation);
        Destroy(instance.gameObject, instance.main.startLifetimeMultiplier);
    }
}
