using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUIElement : Purchasable
{
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text levelsText;

    UpgradeData myData;

    public void SetData(UpgradeData upgradeData)
    {
        myData = upgradeData;
    }

    public override bool Buy()
    {
        int cost = myData.GetNextPowerCostPair().cost;
        if (MoneyManager.HaveEnoughGold(cost) && myData.CanBeUpgraded())
        {
            MoneyManager.SpendGold(cost);
            myData.Upgrade();
            if (!myData.CanBeUpgraded())
                Purchase();

            Refresh();
            Serializer.Serialize();

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
                Debug.Log("Item has been upgraded!");
            }
            else
            {
                Debug.Log("Can't be upgraded!");
                return;
            }
        }
    }

    public override void Refresh()
    {
        PowerAndCostPair nextPowerAndCost = myData.GetNextPowerCostPair();

        thumbnail.sprite = myData.thumbnail;
        costText.text = nextPowerAndCost != null ? myData.GetNextPowerCostPair().cost.ToString("f0") : "-";
        levelsText.text = myData.CanBeUpgraded() ? (myData.CurrentLevel + 1) + " / " + myData.powerAndCost.Count : "MAX!";
    }
}
