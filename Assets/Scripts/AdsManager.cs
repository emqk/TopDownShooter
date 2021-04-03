using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    const string rewardedPlacementId = "Rewarded_Android";
    const string androidID = "4075311";

    int rewardForRewardedAd = 0;
    int skippableAdCounter = 0;
    const int skippableAdCounterThreshold = 3;

    public static AdsManager instance;

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

    public void AdvanceCounter()
    {
        skippableAdCounter += 1;
        Debug.Log("Ad counter advanced " + skippableAdCounter + "/" + skippableAdCounterThreshold);

        if (skippableAdCounter >= skippableAdCounterThreshold)
        {
            if (ShowSkippableAd())
            {
                skippableAdCounter = 0;
            }
        }
    }

    public bool ShowSkippableAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            return true;
        }
        else
        {
            Debug.Log("Skippable ad is not ready");
            return false;
        }
    }

    public bool CanShowRewardedAd()
    {
        return Advertisement.IsReady(rewardedPlacementId);
    }

    public void ShowRewardedAd(int reward)
    {
        if (CanShowRewardedAd())
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
            OnRewardedAdSuccess(placementId);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Ad skipped: " + placementId);
        }
    }

    void OnRewardedAdSuccess(string placementId)
    {
        if (placementId == rewardedPlacementId)
        {
            MoneyManager.AddGold(rewardForRewardedAd);
            Serializer.Serialize();

            //Show popup
            string popupContentText = "You've earned " + rewardForRewardedAd + " from watching ad!";
            PopupData popupData = new PopupData()
            {
                popupType = PopupType.Good,
                title = "Reward received!",
                description = popupContentText,
                buttonsData = new List<PopupButttonData>()
                {
                    new PopupButttonData() { text = "Ok", onClick = PopupManager.instance.CloseLastPopup }
                }
            };
            PopupManager.instance.CreatePopup(popupData);
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
