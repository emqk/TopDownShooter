using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PanelType
{
    None, Maps
}

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text goldText;

    [SerializeField] RectTransform panelParent;
    [SerializeField] MapPurchase mapPurchasePrefab;
    [SerializeField] List<PurchaseData> maps = new List<PurchaseData>();

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
        if (panel == PanelType.Maps)
        {
            SpawnFromList(maps);
        }
    }

    void SpawnFromList(List<PurchaseData> list)
    {
        foreach (PurchaseData data in maps)
        {
            MapPurchase instance = Instantiate(mapPurchasePrefab, panelParent);
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
