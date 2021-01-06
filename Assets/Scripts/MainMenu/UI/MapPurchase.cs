using UnityEngine;

public class MapPurchase : Purchasable
{
    PurchaseData data;
    PanelType type;

    public override bool Buy()
    {
        if (MoneyManager.HaveEnoughGold(data.Cost))
        {
            MoneyManager.SpendGold(data.Cost);
            Purchase();
            Refresh();
            Database.instance.AddPurchaseElementID(data.GetID);
            return true;
        }

        return false;
    }

    public override void Select()
    {
        if (!IsPurchased())
        {
            if (Buy())
            {
                Debug.Log("Item has been bought!");
            }
            else
            {
                Debug.Log("Can't be bought!");
                return;
            }
        }

        if (data.Mesh && data.Material)
        {
            MainMenuUIManager.instance.RefreshVisualizationFromData(data);
        }

        if (type == PanelType.WeaponFirst)
        {
            MainMenuUIManager.instance.RefreshWeaponIcon(data, true);
        }
        else if (type == PanelType.WeaponSecond)
        {
            MainMenuUIManager.instance.RefreshWeaponIcon(data, false);
        }
    }

    public override void Refresh()
    {
        title.text = data.Title;
        thumbnail.sprite = data.Thumbnail;
        description.text = data.Description;

        if (IsPurchased())
        {
            selectText.text = "Use";
        }
        else
        {
            selectText.text = $"Buy ({data.Cost})";
        }
    }

    public void SetData(PurchaseData data, PanelType panelType, bool alreadyPurchased)
    {
        this.data = data;
        type = panelType;
        isPurchased = alreadyPurchased;
    }

    PanelType GetPanelType()
    {
        return type;
    }
}
