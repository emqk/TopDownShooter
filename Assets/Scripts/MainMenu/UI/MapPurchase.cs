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
                Debug.Log("Can't be bought!");
                return;
            }
        }

        MainMenuUIManager.instance.SetCharacterInfoActive(null);
        UpgradeKitData upgradeKitData = Database.instance.GetUpgradeKitData(data.UpgradeKit.ID);

        if (data.Prefab)
        {
            MainMenuUIManager.instance.RefreshVisualizationFromData(data);
        }

        if (type == PanelType.WeaponFirst)
        {
            Database.instance.SetWeaponData((WeaponData)data, true);
            Database.instance.SetWeaponUpgradeKitData(upgradeKitData, true);
            MainMenuUIManager.instance.RefreshWeaponsUI();
        }
        else if (type == PanelType.WeaponSecond)
        {
            Database.instance.SetWeaponData((WeaponData)data, false);
            Database.instance.SetWeaponUpgradeKitData(upgradeKitData, false);
            MainMenuUIManager.instance.RefreshWeaponsUI();
        }
        else if (type == PanelType.Character)
        {
            Database.instance.SetCharacterData((CharacterData)data);
            MainMenuUIManager.instance.SetCharacterInfoActive((CharacterData)data);
        }

        //Upgrades
        MainMenuUIManager.instance.ShowUpgrades(upgradeKitData);

        //Serialize
        Serializer.Serialize();
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
