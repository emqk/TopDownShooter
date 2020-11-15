using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    public static ParticleManager instance;

    void Awake()
    {
        instance = this;
    }

    public void SpawnParticle(Vector3 position, Quaternion rotation)
    {
        ParticleSystem instance = Instantiate(particle, position, rotation);
        Destroy(instance.gameObject, instance.main.startLifetimeMultiplier);
    }
}
