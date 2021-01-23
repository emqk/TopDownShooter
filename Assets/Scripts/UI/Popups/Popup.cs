using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public struct PopupData
{
    public string title;
    public string description;
    public List<PopupButttonData> buttonsData;
}

public struct PopupButttonData
{
    public string text;
    public UnityAction onClick;
}

public class Popup : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] RectTransform buttonsPanel;
    [SerializeField] Button buttonPrefab;

    public void Setup(PopupData popupData)
    {
        ClearPanel();
        RefreshText(popupData);
        SpawnButtons(popupData.buttonsData);
    }

    void RefreshText(PopupData popupData)
    {
        titleText.text = popupData.title;
        descriptionText.text = popupData.description;
    }

    void SpawnButtons(List<PopupButttonData> data)
    {
        foreach (PopupButttonData button in data)
        {
            Button buttonInstance = Instantiate(buttonPrefab, buttonsPanel);
            buttonInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = button.text;
            buttonInstance.onClick.AddListener(button.onClick);
        }
    }

    void ClearPanel() 
    {
        foreach (Transform child in buttonsPanel)
        {
            Destroy(child);
        }
    }
}
