using UnityEngine;

public class ObjectToggler : MonoBehaviour
{
    [SerializeField] float firstTimePeriod = 1f;
    [SerializeField] float enabledTime = 0.5f;
    [SerializeField] float disabledTime = 0.5f;

    void Start()
    {
        Invoke("Disable", firstTimePeriod);
    }

    void Enable()
    {
        gameObject.SetActive(true);
        Invoke("Disable", enabledTime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
        Invoke("Enable", disabledTime);
    }
}
