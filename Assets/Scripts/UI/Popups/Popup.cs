using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum PopupType
{
    Default, Bad, Good
}


public struct PopupData
{
    public PopupType popupType;
    public string title;
    public string description;
    public List<PopupButttonData> buttonsData;
}

public struct PopupButttonData
{
    public string text;
    public UnityAction onClick;
}

[RequireComponent(typeof(AudioSource))]
public class Popup : MonoBehaviour
{
    [SerializeField] Image titleBar;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] RectTransform buttonsPanel;
    [SerializeField] Button buttonPrefab;

    RectTransform blocker;
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Setup(PopupData popupData, RectTransform _blocker, Color titleBarColor, AudioClip audioClip)
    {
        ClearPanel();
        RefreshText(popupData);
        SpawnButtons(popupData.buttonsData);
        blocker = _blocker;
        titleBar.color = titleBarColor;
        audioSource.PlayOneShot(audioClip);
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

    public void Close()
    {
        animator.SetBool("Hide", true);
        blocker.GetComponent<Animator>().SetBool("Hide", true);
        StartCoroutine(CleanupClose());
    }

    IEnumerator CleanupClose()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(gameObject);
        Destroy(blocker.gameObject);
    }
}
