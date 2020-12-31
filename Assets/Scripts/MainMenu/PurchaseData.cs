using UnityEngine;

[CreateAssetMenu(fileName = "NewMapData", menuName = "Maps/NewMapData")]
public class PurchaseData : ScriptableObject
{
    [SerializeField] string title;
    [SerializeField] Sprite thumbnail;
    [SerializeField] int cost;
    [SerializeField] [TextArea] string description;

    public string Title { get => title; }
    public Sprite Thumbnail { get => thumbnail; }
    public int Cost { get => cost; }
    public string Description { get => description; }
}
