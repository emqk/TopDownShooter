using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rewardText;

    int battleReward = 0;
    public static BattleManager instance;

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
