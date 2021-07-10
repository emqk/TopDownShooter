using System.Collections.Generic;
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
            Database.instance.AddPurchaseElementID(data);

            return true;
        }

        return false;
    }

    public override void Select()
    {
        bool alreadyPurchased = Database.instance.IsElementOfIDPurchased(data.GetID);
        if (!alreadyPurchased)
        {
            if (Buy())
            {
                Refresh();
                Debug.Log("Item has been bought!");
            }
            else
            {
                //Popup - Not enough money!
                PopupData popupData = new PopupData()
                {
                    popupType = PopupType.Bad,
                    title = "Can't purchase!",
                    description = "You don't have enough gold to purchase this item!",
                    buttonsData = new List<PopupButttonData>()
                {
                    new PopupButttonData() { text = "Ok", onClick = PopupManager.instance.CloseLastPopup }
                }
                };
                PopupManager.instance.CreatePopup(popupData);

                return;
            }
        }

        // Equip selected item
        MainMenuManager.instance.EquipPurchaseData(data);

        //Show default main menu view
        MainMenuUIManager.instance.ShowMainMenuCanvas();

        //Serialize
        Serializer.Serialize();
    }

    public override void Refresh()
    {
        if(title)
            title.text = data.Title;
        if(thumbnail)
            thumbnail.sprite = data.Thumbnail;
        if(description)
            description.text = data.Description;

        if (Database.instance.IsElementOfIDPurchased(data.GetID))
        {
            selectText.text = "Use";
        }
        else
        {
            selectText.text = $"Buy ({data.Cost})";
        }
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
