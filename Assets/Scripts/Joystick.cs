using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] RectTransform root;
    [SerializeField] RectTransform handle;
    float joystickRadius = 0;

    int fingerOnMe = -1;

    public Vector2 GetResult()
    {
        return new Vector2(handle.localPosition.x / joystickRadius, handle.localPosition.y / joystickRadius);
    }

    public bool IsFingerOnMe()
    {
        return fingerOnMe >= 0;
    }

    void Start()
    {
        joystickRadius = root.sizeDelta.x / 2.0f;
        ResetHandle();
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            //Reset handle if touch ended
            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended && fingerOnMe == touch.fingerId)
            {
                fingerOnMe = -1;
                ResetHandle();
                return;
            }

            //Detect joystick touch start
            if (fingerOnMe < i && touch.phase == TouchPhase.Began && IsInRange(touch.position))
            {
                fingerOnMe = touch.fingerId;
            }

            //Update handle position
            if (fingerOnMe >= i)
            {
                handle.position = touch.position;
            }

            ClampHandle();
        }
    }

    bool IsInRange(Vector2 position)
    {
        float distTohandle = Vector2.Distance(root.position, position);
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
