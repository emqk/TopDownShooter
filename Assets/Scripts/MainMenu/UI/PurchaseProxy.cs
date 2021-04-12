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
        if (index >= purchaseDatas.Count || index < 0)
            return null;

        return purchaseDatas[index];
    }

    public void FocusViewOnElementWithID(string ID)
    {
        int index = FindIndexWithID(ID);
        if (index >= 0)
        {
            view3D.SetFocusElement(index);
        }
    }

    int FindIndexWithID(string ID)
    {
        for (int i = 0; i < purchaseDatas.Count; i++)
        {
            if (purchaseDatas[i].GetID == ID)
            {
                return i;
            }
        }

        return -1;
    }
}
