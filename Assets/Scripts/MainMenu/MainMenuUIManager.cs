using System.Collections.Generic;
using UnityEngine;
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

    public static MainMenuUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RefreshGold();
    }

    void RefreshGold()
    {
        goldText.text = MoneyManager.instance.GetGoldAmount().ToString();
    }


    public void ShowPanel(int panelType)
    {
        ClearPanel();

        PanelType panel = (PanelType)panelType;
        switch (panel)
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

    void SpawnFromList(List<PurchaseData> list)
    {
        foreach (PurchaseData data in list)
        {
            MapPurchase instance = Instantiate(purchasePrefab, panelParent);
            instance.SetMapData(data);
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
