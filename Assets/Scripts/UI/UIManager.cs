using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] WeaponHeatImage weaponHeatImage;
    [SerializeField] EndPanelUI endPanelPrefab;

    [SerializeField] Canvas targetCanvas;

    EndPanelUI endPanelInstance;
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
        if (endPanelInstance)
        {
            Debug.Log("Can't show end panel - is already opened!");
            return;
        }

        endPanelInstance = Instantiate(endPanelPrefab, targetCanvas.transform);
        endPanelInstance.Open();
    }
}
