using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    const string rewardedPlacementId = "Rewarded_Android";
    static string androidID = "4075311";
    public static AdsManager instance;
    int rewardForRewardedAd = 0;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Initialize()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(androidID, true);
    }

    public void ShowSkippableAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Skippable ad is not ready");
        }
    }

    public void ShowRewardedAd(int reward)
    {
        if (Advertisement.IsReady(rewardedPlacementId))
        {
            rewardForRewardedAd = reward;
            Advertisement.Show(rewardedPlacementId);
        }
        else
        {
            Debug.Log("Rewarded ad is not ready");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ad error: " + message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Failed)
        {
            Debug.Log("Ad failed: " + placementId);
        }
        else if (showResult == ShowResult.Finished)
        {
            Debug.Log("Ad finished: " + placementId);
            if (placementId == rewardedPlacementId)
            {
                MoneyManager.AddGold(rewardForRewardedAd);
                Serializer.Serialize();
                Debug.Log("Added gold from ad!");
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Ad skipped: " + placementId);
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log("Ad start: " + placementId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("Ad ready: " + placementId);
    }
}
