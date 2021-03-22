using UnityEngine;

public class BattleManager : MonoBehaviour
{
    int battleReward = 0;

    public static BattleManager instance;

    private void Awake()
    {
        instance = this;
    }

    public int GetReward()
    {
        return battleReward;
    }

    public void AddToReward(int amount)
    {
        battleReward += amount;
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
