using UnityEngine;

public class RadialDamageVisualizer : MonoBehaviour
{
    public void Init(float radius, float lifetime = 1)
    {
        transform.localScale = new Vector3(radius, radius, radius);
        Destroy(gameObject, lifetime);
    }
}
