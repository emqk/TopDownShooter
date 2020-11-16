using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 dragDistanceUnscaled = touch.deltaPosition;
            float dragDistanceScaledX = dragDistanceUnscaled.x / Screen.width;
            transform.Rotate(Vector3.up, -dragDistanceScaledX * rotateSpeed * Time.deltaTime);
        }    
    }
}
