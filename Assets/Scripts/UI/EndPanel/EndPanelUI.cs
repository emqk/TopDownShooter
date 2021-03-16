using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndPanelUI : MonoBehaviour
{
    [SerializeField] Color wavesCompletedBackgroundColor;
    [SerializeField] Color wavesNotCompletedBackgroundColor;
    [SerializeField] Image wavesBackgroundImage;
    [SerializeField] TextMeshProUGUI wavesText;
    [SerializeField] TextMeshProUGUI moneyText;

    public void Refresh()
    {
        int waveIndex = SpawnManager.instance.GetCurrentWaveIndex();
        int numOfWaves = SpawnManager.instance.GetNumberOfWaves();
        if (waveIndex + 1 >= numOfWaves)
        {
            wavesBackgroundImage.color = wavesCompletedBackgroundColor;
            wavesText.text = "Map completed!";
        }
        else
        {
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
