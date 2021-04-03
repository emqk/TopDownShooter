using UnityEngine;

public class RewardButtonPanelUI : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        int rewardFromAd = (int)(BattleManager.instance.GetReward() * 0.5f);
        AdsManager.instance.ShowRewardedAd(rewardFromAd);

        gameObject.SetActive(false);
    }
}
