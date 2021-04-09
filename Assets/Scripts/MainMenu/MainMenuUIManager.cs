using System.Collections.Generic;
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
    [SerializeField] TMP_Text characterNameText;
    [SerializeField] Transform characterVisualizationObjParent;
    [SerializeField] TMP_Text weaponNameText;
    [SerializeField] Image weaponThumbnail;
    [SerializeField] TMP_Text mapNameText;
    [SerializeField] Image mapThumbnail;

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
            weaponNameText.text = data.Title;
            weaponThumbnail.sprite = data.Thumbnail;
        }
        else
        {
            Debug.Log("Can't refresh weapon icon - data is null!");
        }
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

        foreach (Transform child in characterVisualizationObjParent)
        {
            Destroy(child.gameObject);
        }

        characterNameText.text = data.Title;
        Instantiate(data.Prefab, characterVisualizationObjParent);
    }

    public void RefreshMapUI()
    {
        MapData mapData = Database.instance.GetMapData();

        if (mapData)
        {
            mapNameText.text = mapData.Title;
            mapThumbnail.sprite = mapData.Thumbnail;
        }
        else
        {
            Debug.LogError("Can't refresh map data - data is null!");
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
