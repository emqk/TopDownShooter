using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] FillUI healthUI;
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Joystick rotationJoystick;

    [Header("Other")]
    [SerializeField] Canvas mainBattleCanvas;
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

    public void ShowEndPanel()
    {
        if (endPanelInstance)
        {
            Debug.Log("Can't show end panel - is already opened!");
            return;
        }

        mainBattleCanvas.gameObject.SetActive(false);

        endPanelInstance = Instantiate(endScreenCanvasPrefab);
        endPanelInstance.Open();
    }
}
