using UnityEngine;
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
        int cost = myData.powerAndCost[myData.CurrentLevel].cost;
        if (MoneyManager.HaveEnoughGold(cost))
        {
            MoneyManager.SpendGold(cost);
            if (myData.CanBeUpgraded())
            {
                myData.Upgrade();
                if (!myData.CanBeUpgraded())
                    Purchase();
            }

            Refresh();

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
        thumbnail.sprite = myData.thumbnail;
        costText.text = myData.powerAndCost[myData.CurrentLevel].cost.ToString("f0");
        levelsText.text = myData.CurrentLevel + " / " + myData.powerAndCost.Count;
    }
}
