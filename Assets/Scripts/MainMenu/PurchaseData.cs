using UnityEngine;

[CreateAssetMenu(fileName = "NewPurchaseData", menuName = "Purchase/NewPurchaseData")]
public class PurchaseData : ScriptableObject
{
    [Header("Purchase data")]
    [SerializeField] string ID;
    [SerializeField] string title;
    [SerializeField] Sprite thumbnail;
    [SerializeField] int cost;
    [SerializeField] [TextArea] string description;

    [SerializeField] GameObject prefab;

    [SerializeField] UpgradeKitData upgradeKit;

    public string Title { get => title; }
    public Sprite Thumbnail { get => thumbnail; }
    public int Cost { get => cost; }
    public string Description { get => description; }

    public GameObject Prefab { get => prefab; }
    public string GetID { get => ID; }
    public UpgradeKitData UpgradeKit { get => upgradeKit; }
}
