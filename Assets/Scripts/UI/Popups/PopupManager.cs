using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] Popup popupPrefab;
    [SerializeField] RectTransform blockerPanel;
    [SerializeField] Canvas targetCanvas;

    List<Popup> popups = new List<Popup>();

    public static PopupManager instance;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("One PopupManager is already on this scene. Destroying!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PopupData popupData = new PopupData()
        {
            title = "Test title",
            description = "Test description",
            buttonsData = new List<PopupButttonData>()
            {
                new PopupButttonData() { text = "Show debug", onClick = () => Debug.Log("Clicked button 0") },
                new PopupButttonData() { text = "Close", onClick = () => Debug.Log("close") }
            }
        };

        CreatePopup(popupData);
    }

    public void CreatePopup(PopupData popupData)
    {
        RectTransform backgroundBlockerInstance = Instantiate(blockerPanel, targetCanvas.transform);
        Popup popupInstance = Instantiate(popupPrefab, targetCanvas.transform);
        popupInstance.Setup(popupData, backgroundBlockerInstance);
        popups.Add(popupInstance);
    }
}
