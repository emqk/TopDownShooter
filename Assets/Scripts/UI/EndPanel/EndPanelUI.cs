using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndPanelUI : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] Color wavesCompletedBackgroundColor;
    [SerializeField] Color wavesNotCompletedBackgroundColor;
    [SerializeField] Image wavesBackgroundImage;
    [SerializeField] TextMeshProUGUI wavesText;

    [Header("Other")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI moneyText;

    public void Open()
    {
        BattleManager.instance.Pause();
        Refresh();
    }

    void Refresh()
    {
        int waveIndex = SpawnManager.instance.GetCurrentWaveIndex();
        int numOfWaves = SpawnManager.instance.GetNumberOfWaves();
        if (waveIndex >= numOfWaves)
        {
            titleText.text = "Victory!";
            wavesBackgroundImage.color = wavesCompletedBackgroundColor;
            wavesText.text = "Map completed!";
        }
        else
        {
            titleText.text = "Try again next time...";
            wavesBackgroundImage.color = wavesNotCompletedBackgroundColor;
            wavesText.text = waveIndex + " / " + numOfWaves;
        }

        moneyText.text = "Testing from code: 123";
    }

    void SetColorToWavesBackground(Color color) 
    {
        wavesBackgroundImage.color = color;
    }
}
