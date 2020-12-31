using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapPurchase : Purchasable
{
    [SerializeField] MapData mapData;

    public override void Buy()
    {
        throw new System.NotImplementedException();
    }

    public override void Select()
    {
        throw new System.NotImplementedException();
    }

    public override void Refresh()
    {
        title.text = mapData.Title;
        thumbnail.sprite = mapData.Thumbnail;
        description.text = mapData.Description;

        selectText.text = $"Buy ({mapData.Cost})";
    }
}
