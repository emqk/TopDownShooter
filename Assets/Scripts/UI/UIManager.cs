using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] WeaponHeatImage weaponHeatImage;
    [SerializeField] EndPanelUI endPanelPrefab;

    [SerializeField] Canvas targetCanvas;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public WeaponHeatImage GetWeaponHeatImage()
    {
        return weaponHeatImage;
    }

    public void ShowEndPanel()
    {
        EndPanelUI endPanelInstance = Instantiate(endPanelPrefab, targetCanvas.transform);
        endPanelInstance.Refresh();
    }
}
