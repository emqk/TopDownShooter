using UnityEngine;
using TMPro;

public struct ScreenNotificationData
{
    public string notificationText;
    public float displayDuration;
}

public class ScreenNotification : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textUI;

    float timeToEnd;

    public void Init(ScreenNotificationData notificationData)
    {
        textUI.text = notificationData.notificationText;
        timeToEnd = notificationData.displayDuration;

        Show();
    }

    void Update()
    {
        if (!gameObject.activeSelf)
            return;

        if (timeToEnd <= 0.0f)
        {
            Hide();
        }
        else
        {
            timeToEnd -= Time.deltaTime;
        }
    }

    void Show()
    {
        if (gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
    }

    void Hide()
    {
        if (!gameObject.activeSelf)
            return;

        gameObject.SetActive(false);
    }
}
