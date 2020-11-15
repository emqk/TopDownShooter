using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 lastFramePos = new Vector3();
    RaycastHit hit = new RaycastHit();

    private void Start()
    {
        lastFramePos = transform.position;
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        transform.position += transform.forward * 40 * Time.deltaTime;
        CheckCollision();
        lastFramePos = transform.position;
    }

    void CheckCollision()
    {
        if (Physics.Linecast(lastFramePos, transform.position, out hit))
        {
            ParticleManager.instance.SpawnParticle(hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(gameObject);
        }
    }
}
