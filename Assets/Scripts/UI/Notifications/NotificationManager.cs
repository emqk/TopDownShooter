using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] ScreenNotification screenNotification;
    [SerializeField] TMP_Text damageTextPrefab;
    [SerializeField] Camera textLookCamera;
    List<TMP_Text> damageInfoInstances = new List<TMP_Text>();

    float textMoveUpSpeed = 1;
    float textAlphaChangeSpeed = 1.5f;

    public static NotificationManager instance;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        foreach (TMP_Text item in damageInfoInstances)
        {
            //Look at camera
            Vector3 dir = (item.transform.position - textLookCamera.transform.position).normalized;
            item.transform.rotation = Quaternion.LookRotation(dir);

            //Update position
            item.transform.position += new Vector3(0, textMoveUpSpeed * Time.deltaTime, 0);

            //Update color
            Color currentColor = item.color;
            item.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a -= Time.deltaTime * textAlphaChangeSpeed);
        }
    }

    public void SpawnDamageInfo(Vector3 location, int damage)
    {
        TMP_Text damageInfoInstance = Instantiate(damageTextPrefab);
        damageInfoInstance.transform.position = location;
        damageInfoInstance.text = damage.ToString("f0");
        damageInfoInstances.Add(damageInfoInstance);

        StartCoroutine(WaitAndDestroy(damageInfoInstance, 5));
    }

    IEnumerator WaitAndDestroy(TMP_Text infoInstance, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        damageInfoInstances.Remove(infoInstance);
        Destroy(infoInstance.gameObject);
    }


    public void ShowNotification(ScreenNotificationData screenNotificationData)
    {
        screenNotification.Init(screenNotificationData);
    }
}
