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
            Purchase();
            Database.instance.AddPurchaseElementID(data);

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

        UpgradeKitData upgradeKitData = Database.instance.GetUpgradeKitDataByID(data.UpgradeKit.ID);

        if (type != PanelType.Map)
        {
            MainMenuUIManager.instance.SetCharacterInfoActive(null);
            MainMenuUIManager.instance.ShowUpgrades(upgradeKitData);

            if (data.Prefab)
            {
                MainMenuUIManager.instance.RefreshVisualizationFromData(data);
            }
        }

        if (type == PanelType.Weapon)
        {
            Database.instance.SetWeaponData((WeaponData)data);
            Database.instance.SetWeaponUpgradeKitData(upgradeKitData);
            MainMenuUIManager.instance.RefreshWeaponsUI();
        }
        else if (type == PanelType.Character)
        {
            Database.instance.SetCharacterData((CharacterData)data);
            MainMenuUIManager.instance.SetCharacterInfoActive((CharacterData)data);
        }
        else if (type == PanelType.Map)
        {
            Database.instance.SetMapData((MapData)data);
        }

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
