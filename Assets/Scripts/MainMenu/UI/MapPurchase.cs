public class MapPurchase : Purchasable
{
    PurchaseData data;
    PanelType type;

    public override void Buy()
    {
        throw new System.NotImplementedException();
    }

    public override void Select()
    {
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

        selectText.text = $"Buy ({data.Cost})";
    }

    public void SetData(PurchaseData data, PanelType panelType)
    {
        this.data = data;
        type = panelType;
    }

    PanelType GetPanelType()
    {
        return type;
    }
}
