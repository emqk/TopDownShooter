using UnityEngine;

[CreateAssetMenu(fileName = "NewPurchaseData", menuName = "Purchase/NewPurchaseData")]
public class PurchaseData : ScriptableObject
{
    [SerializeField] string title;
    [SerializeField] Sprite thumbnail;
    [SerializeField] int cost;
    [SerializeField] [TextArea] string description;

    [SerializeField] Mesh mesh;
    [SerializeField] Material material;

    public string Title { get => title; }
    public Sprite Thumbnail { get => thumbnail; }
    public int Cost { get => cost; }
    public string Description { get => description; }

    public Mesh Mesh { get => mesh; }
    public Material Material { get => material; }
}
