using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PanelType
{
    None, Character, Map, WeaponFirst, WeaponSecond
}

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text goldText;

    [SerializeField] RectTransform purchaseElementsPanel;
    [SerializeField] RectTransform upgradeElementsPanel;
    [SerializeField] MapPurchase purchasePrefab;
    [SerializeField] UpgradeUIElement upgradeUIPrefab;

    [Header("Data")]
    [SerializeField] List<PurchaseData> characters = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> maps = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> weaponsFirst = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> weaponsSecond = new List<PurchaseData>();

    [Header("Mesh visualization")]
    [SerializeField] Transform visualizationObjParent;
    [SerializeField] Image weaponFirstThumbnail;
    [SerializeField] Image weaponSecondThumbnail;

    [Header("Character info")]
    [SerializeField] RectTransform characterInfoPanel;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text movementSpeedText;

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

    void RefreshWeaponIcon(PurchaseData data, bool isItFirst)
    {
        if (isItFirst)
        {
            weaponFirstThumbnail.sprite = data.Thumbnail;
        }
        else
        {
            weaponSecondThumbnail.sprite = data.Thumbnail;
        }
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
            case PanelType.WeaponFirst: SpawnPurchasableFromList(weaponsFirst);
                break;
            case PanelType.WeaponSecond:SpawnPurchasableFromList(weaponsSecond);
                break;
            default:
                break;
        }
    }

    public void RefreshWeaponsUI()
    {
        WeaponData firstWeapon = Database.instance.GetWeaponData(true);
        WeaponData secondWeapon = Database.instance.GetWeaponData(false);

        if (firstWeapon)
            RefreshWeaponIcon(firstWeapon, true);
        if (secondWeapon)
            RefreshWeaponIcon(secondWeapon, false);
    }

    public void RefreshCharacterSkin()
    {
        CharacterData characterData = Database.instance.GetCharacterData();
        RefreshVisualizationFromData(characterData);
        SetCharacterInfoActive(characterData);
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

    public void SetCharacterInfoActive(CharacterData characterData)
    {
        if (characterData)
        {
            characterInfoPanel.gameObject.SetActive(true);
            healthText.text = characterData.MaxHealth.ToString();
            movementSpeedText.text = characterData.MovementSpeed.ToString();
        }
        else
        {
            characterInfoPanel.gameObject.SetActive(false);
        }
    }
}
