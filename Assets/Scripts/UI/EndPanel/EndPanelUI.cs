using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndPanelUI : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] Color wavesCompletedBackgroundColor;
    [SerializeField] Color wavesNotCompletedBackgroundColor;
    [SerializeField] Image wavesBackgroundImage;
    [SerializeField] TextMeshProUGUI wavesText;

    [Header("Other")]
    [SerializeField] RewardButtonPanelUI rewardButtonPanel;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] LoadingScreenPanelUI loadingScreenPrefab;

    public void Open()
    {
        BattleManager.instance.Pause();
        Refresh();
        AddEarnedReward();
    }

    public void LoadMainMenu()
    {
        Instantiate(loadingScreenPrefab);
        StartCoroutine(DelayedLoadMainMenu());
    }

    IEnumerator DelayedLoadMainMenu()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("MainMenu");
    }

    void AddEarnedReward()
    {
        Debug.Log("Adding reward");
        MoneyManager.AddGold(BattleManager.instance.GetReward());
        Serializer.Serialize();
    }

    void Refresh()
    {
        int waveIndex = SpawnManager.instance.GetCurrentWaveIndex();
        int numOfWaves = SpawnManager.instance.GetNumberOfWaves();
        if (waveIndex >= numOfWaves)
        {
            titleText.text = "Victory!";
            SetColorToWavesBackground(wavesCompletedBackgroundColor);
            wavesText.text = "Map completed!";
        }
        else
        {
            titleText.text = "Try again next time...";
            SetColorToWavesBackground(wavesNotCompletedBackgroundColor);
            wavesText.text = waveIndex + " / " + numOfWaves;
        }

        moneyText.text = BattleManager.instance.GetReward().ToString();

        if (AdsManager.instance.CanShowRewardedAd())
        {
            rewardButtonPanel.gameObject.SetActive(true);
        }
        else
        {
            rewardButtonPanel.gameObject.SetActive(false);
        }
    }

    void SetColorToWavesBackground(Color color) 
    {
        wavesBackgroundImage.color = color;
    }
}
