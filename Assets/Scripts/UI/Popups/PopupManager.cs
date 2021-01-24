﻿using System.Collections.Generic;
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

    public void CloseLastPopup()
    {
        if (popups.Count <= 0)
        {
            Debug.Log("Can't close the last popup - popups count <= 0!");
            return;
        }

        popups[popups.Count - 1].Close();
    }

    public void CreatePopup(PopupData popupData)
    {
        RectTransform backgroundBlockerInstance = Instantiate(blockerPanel, targetCanvas.transform);
        Popup popupInstance = Instantiate(popupPrefab, targetCanvas.transform);
        popupInstance.Setup(popupData, backgroundBlockerInstance);
        popups.Add(popupInstance);
    }
}
