using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        transform.position += transform.forward * 30 * Time.deltaTime;       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
