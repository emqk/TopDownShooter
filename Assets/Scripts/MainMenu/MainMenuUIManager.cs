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

    [SerializeField] RectTransform panelParent;
    [SerializeField] MapPurchase purchasePrefab;

    [SerializeField] List<PurchaseData> characters = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> maps = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> weaponsFirst = new List<PurchaseData>();
    [SerializeField] List<PurchaseData> weaponsSecond = new List<PurchaseData>();

    [SerializeField] GameObject visualizationObj;
    [SerializeField] Image weaponFirstThumbnail;
    [SerializeField] Image weaponSecondThumbnail;


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

    public void RefreshWeaponIcon(PurchaseData data, bool isItFirst)
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
        ClearPanel();

        currentPanelType = (PanelType)panelType;
        switch (currentPanelType)
        {
            case PanelType.None:
                break;
            case PanelType.Character:   SpawnFromList(characters);
                break;
            case PanelType.Map:         SpawnFromList(maps);
                break;
            case PanelType.WeaponFirst: SpawnFromList(weaponsFirst);
                break;
            case PanelType.WeaponSecond:SpawnFromList(weaponsSecond);
                break;
            default:
                break;
        }
    }

    public void RefreshVisualizationFromData(PurchaseData data)
    {
        visualizationObj.GetComponent<MeshFilter>().sharedMesh = data.Mesh;
        visualizationObj.GetComponent<MeshRenderer>().sharedMaterial = data.Material;
    }

    void SpawnFromList(List<PurchaseData> list)
    {
        foreach (PurchaseData data in list)
        {
            MapPurchase instance = Instantiate(purchasePrefab, panelParent);
            bool alreadyPurchased = Database.instance.IsElementOfIDPurchased(data.GetID);
            instance.SetData(data, currentPanelType, alreadyPurchased);
            instance.Refresh();
        }
    }

    void ClearPanel()
    {
        foreach (Transform child in panelParent)
        {
            Destroy(child.gameObject);
        }
    }
}
