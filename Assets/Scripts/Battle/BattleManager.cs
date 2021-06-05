using UnityEngine;

public class BattleManager : MonoBehaviour
{
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
        UIManager.instance.RefreshRewardText(battleReward, false);
    }

    public void EndBattle()
    {
        UIManager.instance.ShowEndPanel();
    }

    public int GetReward()
    {
        return battleReward;
    }

    public void AddToReward(int amount)
    {
        battleReward += amount;
        UIManager.instance.RefreshRewardText(battleReward);
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
