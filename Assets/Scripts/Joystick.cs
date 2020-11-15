using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] RectTransform background;
    [SerializeField] RectTransform handle;
    float joystickRadius = 0;

    int fingerOnMe = -1;

    public Vector2 GetResult()
    {
        return new Vector2(handle.localPosition.x / joystickRadius, handle.localPosition.y / joystickRadius);
    }

    void Start()
    {
        joystickRadius = background.sizeDelta.x / 2.0f;
        ResetHandle();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                fingerOnMe = -1;
                ResetHandle();
                return;
            }

            if (fingerOnMe < 0 && touch.phase == TouchPhase.Began && IsInRange(touch.position))
            {
                fingerOnMe = touch.fingerId;
            }
            
            if (fingerOnMe >= 0)
            {
                handle.position = touch.position;
            }

            ClampHandle();
        }
    }

    bool IsInRange(Vector2 position)
    {
        float distTohandle = Vector2.Distance(background.position, position);
        return distTohandle <= joystickRadius;
    }

    void ClampHandle()
    {
        if (!IsInRange(handle.position))
        {
            Vector2 dirToHandle = handle.localPosition.normalized;
            handle.localPosition = dirToHandle * joystickRadius;
        }
    }

    void ResetHandle()
    {
        handle.localPosition = Vector2.zero;
    }
}
