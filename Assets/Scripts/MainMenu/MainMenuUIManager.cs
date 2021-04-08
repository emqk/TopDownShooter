﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PanelType
{
    None, Character, Map, Weapon
}

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text goldText;

    [SerializeField] RectTransform mapsUI;
    [SerializeField] RectTransform purchaseElementsPanel;
    [SerializeField] RectTransform upgradeElementsPanel;
    [SerializeField] MapPurchase purchasePrefab;
    [SerializeField] UpgradeUIElement upgradeUIPrefab;

    [Header("Shop")]
    [SerializeField] GameObject playerRepresentation;
    [SerializeField] PurchaseProxy purchaseProxy;
    [SerializeField] MapPurchase itemInfoPanel;

    [Header("Canvases")]
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas shopCanvas;

    [Header("Data")]
    [SerializeField] List<PurchaseData> characters = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> maps = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> weapons = new List<PurchaseData>();

    [Header("Mesh visualization")]
    [SerializeField] Transform visualizationObjParent;
    [SerializeField] Image weaponThumbnail;

    PanelType currentPanelType = PanelType.None;

    public static MainMenuUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RefreshGold();
    }

    public void RefreshGold()
    {
        goldText.text = MoneyManager.GetGoldAmount().ToString();
    }

    void RefreshWeaponIcon(PurchaseData data)
    {
        if (data)
        {
            weaponThumbnail.sprite = data.Thumbnail;
        }
        else
        {
            Debug.Log("Can't refresh weapon icon - data is null!");
        }
    }

    public void ToggleMapsUI()
    {
        if (mapsUI.gameObject.activeSelf)
        {
            HideMaps();
        }
        else
        {
            ShowMaps();
        }
    }

    void ShowMaps()
    {
        ClearPurchasePanel();
        currentPanelType = PanelType.Map;
        SpawnPurchasableFromList(maps);
        mapsUI.gameObject.SetActive(true);
    }

    void HideMaps()
    {
        mapsUI.gameObject.SetActive(false);
    }

    public void ShowWeapons()
    {
        currentPanelType = PanelType.Weapon;
        SpawnDataPrefabsOnView3D(weapons);
    }

    public void ShowCharacters()
    {
        currentPanelType = PanelType.Character;
        SpawnDataPrefabsOnView3D(characters);
    }

    public void ShowMapsNew()
    {
        currentPanelType = PanelType.Map;
        SpawnDataPrefabsOnView3D(maps);
    }

    public void RefreshShopItemInfo()
    {
        PurchaseData purchaseData = purchaseProxy.GetAtIndex(purchaseProxy.View3D.GetSelectedIndex());

        if (purchaseData)
        {
            bool alreadyPurchased = Database.instance.IsElementOfIDPurchased(purchaseData.GetID);
            itemInfoPanel.SetData(purchaseData, currentPanelType, alreadyPurchased);
            itemInfoPanel.Refresh();
        }
    }

    void SpawnDataPrefabsOnView3D(List<PurchaseData> data)
    {
        GameObject[] objs = new GameObject[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            objs[i] = data[i].Prefab;
        }

        purchaseProxy.SetPurchaseData(data);
        purchaseProxy.View3D.SetContentObjects(objs);
    }

    public void ShowPanel(int panelType)
    {
        ClearPurchasePanel();

        currentPanelType = (PanelType)panelType;
        switch (currentPanelType)
        {
            case PanelType.None:
                break;
            case PanelType.Character:   SpawnPurchasableFromList(characters);
                break;
            case PanelType.Map:         SpawnPurchasableFromList(maps);
                break;
            case PanelType.Weapon:      SpawnPurchasableFromList(weapons);
                break;
            default:
                break;
        }
    }

    public void RefreshWeaponsUI()
    {
        WeaponData weapon = Database.instance.GetWeaponData();
        UpgradeKitData upgradeKitData = Database.instance.GetUpgradeKitDataByID(weapon.UpgradeKit.ID);

        RefreshWeaponIcon(weapon);
        ShowUpgrades(upgradeKitData);
    }

    public void RefreshCharacterSkin()
    {
        CharacterData characterData = Database.instance.GetCharacterData();
        RefreshVisualizationFromData(characterData);
    }

    public void RefreshVisualizationFromData(PurchaseData data)
    {
        if (!data)
        {
            Debug.LogError("Can't visualize from data - data is null!");
            return;
        }

        foreach (Transform child in visualizationObjParent)
        {
            Destroy(child.gameObject);
        }

        Instantiate(data.Prefab, visualizationObjParent);
    }

    void SpawnPurchasableFromList(List<PurchaseData> list)
    {
        foreach (PurchaseData data in list)
        {
            MapPurchase instance = Instantiate(purchasePrefab, purchaseElementsPanel);
            bool alreadyPurchased = Database.instance.IsElementOfIDPurchased(data.GetID);
            instance.SetData(data, currentPanelType, alreadyPurchased);
            instance.Refresh();
        }
    }

    void ClearPurchasePanel()
    {
        foreach (Transform child in purchaseElementsPanel)
        {
            Destroy(child.gameObject);
        }
    }


    public void ShowUpgrades(UpgradeKitData upgradeKitData)
    {
        if (upgradeKitData == null)
        {
            Debug.LogError("Can't refresh - upgradeData is null!");
            return;
        }

        //Clear panel
        foreach (Transform child in upgradeElementsPanel)
        {
            Destroy(child.gameObject);
        }

        //Spawn panels
        foreach (UpgradeDataInstance dataInstance in Database.instance.GetUpgradeKitDataByID(upgradeKitData.ID).upgradeDatas)
        {
            UpgradeUIElement instance = Instantiate(upgradeUIPrefab, upgradeElementsPanel);
            instance.SetData(dataInstance);
            instance.Refresh();
        }
    }

    public void ShowMainMenuCanvas()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        shopCanvas.gameObject.SetActive(false);
        playerRepresentation.SetActive(true);
        purchaseProxy.gameObject.SetActive(false);
    }

    public void ShowShopCanvas()
    {
        shopCanvas.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
        playerRepresentation.SetActive(false);
        purchaseProxy.gameObject.SetActive(true);
    }
}
