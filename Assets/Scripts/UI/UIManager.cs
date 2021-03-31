using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] FillUI healthUI;
    [SerializeField] WeaponHeatImage weaponHeatImage;
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Joystick rotationJoystick;

    [Header("Other")]
    [SerializeField] EndPanelUI endScreenCanvasPrefab;

    EndPanelUI endPanelInstance;
    public static UIManager instance;

    public FillUI HealthUI { get => healthUI; }
    public Joystick MovementJoystick { get => movementJoystick; }
    public Joystick RotationJoystick { get => rotationJoystick; }

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

        endPanelInstance = Instantiate(endScreenCanvasPrefab);
        endPanelInstance.Open();
    }
}
