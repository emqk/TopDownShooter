using UnityEngine;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text goldText;

    public static MainMenuUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RefreshGold();
    }

    void RefreshGold()
    {
        goldText.text = MoneyManager.instance.GetGoldAmount().ToString();
    }
}
