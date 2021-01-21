using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] Popup popupPrefab;
    [SerializeField] Canvas targetCanvas;

    

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
                new PopupButttonData() { text = "Button 0", onClick = () => Debug.Log("Clicked button 0") },
                new PopupButttonData() { text = "Button 1", onClick = () => Debug.Log("Clicked button 1") }
            }
        };

        CreatePopup(popupData);
    }

    public void CreatePopup(PopupData popupData)
    {
        Popup popupInstance = Instantiate(popupPrefab, targetCanvas.transform);
        popupInstance.Setup(popupData);
    }
}
