using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] Camera mainCamera;

    int battleReward = 0;
    public static BattleManager instance;

    public Camera MainCamera { get => mainCamera; }


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RefreshRewardText();
    }

    public int GetReward()
    {
        return battleReward;
    }

    public void AddToReward(int amount)
    {
        battleReward += amount;
        RefreshRewardText();
    }

    void RefreshRewardText()
    {
        rewardText.text = battleReward.ToString();
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
}
