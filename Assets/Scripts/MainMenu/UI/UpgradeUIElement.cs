using UnityEngine;
using TMPro;

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
        int cost = myDataInstance.GetNextPowerCostPair().cost;
        if (MoneyManager.HaveEnoughGold(cost) && myDataInstance.CanBeUpgraded())
        {
            MoneyManager.SpendGold(cost);
            myDataInstance.Upgrade();
            if (!myDataInstance.CanBeUpgraded())
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
        PowerAndCostPair nextPowerAndCost = myDataInstance.GetNextPowerCostPair();

        thumbnail.sprite = myDataInstance.Thumbnail;
        costText.text = nextPowerAndCost != null ? myDataInstance.GetNextPowerCostPair().cost.ToString("f0") : "-";
        levelsText.text = myDataInstance.CanBeUpgraded() ? (myDataInstance.CurrentLevel + 1) + " / " + myDataInstance.PowerAndCost.Count : "MAX!";
    }
}
