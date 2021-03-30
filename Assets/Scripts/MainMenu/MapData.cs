using UnityEngine;

[CreateAssetMenu(fileName = "NewMapData", menuName = "Maps/Create New Map")]
public class MapData : PurchaseData
{
    [Header("--------------------------------- [ MAP INFO ] ---------------------------------")]
    [SerializeField] string sceneName;

    public string SceneName { get => sceneName; }
}
