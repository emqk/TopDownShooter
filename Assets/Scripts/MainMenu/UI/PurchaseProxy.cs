using System.Collections.Generic;
using UnityEngine;

public class PurchaseProxy : MonoBehaviour
{
    public HorizontalView3D View3D { get => view3D; }
    [SerializeField] HorizontalView3D view3D;

    List<PurchaseData> purchaseDatas = new List<PurchaseData>();


    public void SetPurchaseData(List<PurchaseData> datas)
    {
        purchaseDatas = datas;
    }

    public PurchaseData GetAtIndex(int index)
    {
        if (index >= purchaseDatas.Count)
            return null;

        return purchaseDatas[index];
    }
}
