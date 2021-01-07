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
        throw new System.NotImplementedException();
    }

    public override void Select()
    {
        throw new System.NotImplementedException();
    }

    public override void Refresh()
    {
        thumbnail.sprite = myData.thumbnail;
        costText.text = myData.powerAndCost[myData.currentLevel].cost.ToString("f0");
        levelsText.text = myData.currentLevel + " / " + myData.powerAndCost.Count;
    }
}
