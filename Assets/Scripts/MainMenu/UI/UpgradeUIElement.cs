using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UpgradeUIElement : Purchasable
{
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text levelsText;

    UpgradeDataInstance myDataInstance;

    public void SetData(UpgradeDataInstance upgradeInstance)
    {
        myDataInstance = upgradeInstance;
    }

    public override bool Buy()
    {
        bool canUpgrade = myDataInstance.CanBeUpgraded();
        if (!canUpgrade)
        {
            //Popup - upgrade is at max level!
            PopupData popupData = new PopupData()
            {
                popupType = PopupType.Good,
                title = "Already upgraded",
                description = "This upgrade is on the maximum level!",
                buttonsData = new List<PopupButttonData>()
                {
                    new PopupButttonData() { text = "Ok", onClick = PopupManager.instance.CloseLastPopup }
                }
            };
            PopupManager.instance.CreatePopup(popupData);
            return false;
        }

        int cost = myDataInstance.GetNextPowerCostPair().cost;
        bool haveGold = MoneyManager.HaveEnoughGold(cost);
        if (haveGold && canUpgrade)
        {
            MoneyManager.SpendGold(cost);
            myDataInstance.Upgrade();

            return true;
        }

        if (!haveGold)
        {
            //Popup - Not enough money!
            PopupData popupData = new PopupData()
            {
                popupType = PopupType.Bad,
                title = "Can't upgrade!",
                description = "You don't have enough gold to purchase this upgrade!",
                buttonsData = new List<PopupButttonData>()
                {
                    new PopupButttonData() { text = "Ok", onClick = PopupManager.instance.CloseLastPopup }
                }
            };
            PopupManager.instance.CreatePopup(popupData);
            return false;
        }

        return false;
    }

    public override void Select()
    {
        if (Buy())
        {
            Refresh();
            Serializer.Serialize();
            Debug.Log("Item has been upgraded!");
        }
        else
        {
            Debug.Log("Can't be upgraded!");
            return;
        }
    }

    public override void Refresh()
    {
        PowerAndCostPair nextPowerAndCost = myDataInstance.GetNextPowerCostPair();

        thumbnail.sprite = myDataInstance.Thumbnail;
        costText.text = nextPowerAndCost != null ? myDataInstance.GetNextPowerCostPair().cost.ToString("f0") : "-";
        levelsText.text = myDataInstance.CanBeUpgraded() ? (myDataInstance.CurrentLevel + 1) + " / " + myDataInstance.PowerAndCost.Count : "MAX!";
    }
}
