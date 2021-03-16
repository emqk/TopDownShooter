using UnityEngine;
using TMPro;

public class EndPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI wavesText;
    [SerializeField] TextMeshProUGUI moneyText;

    public void Refresh()
    {
        int waveIndex = SpawnManager.instance.GetCurrentWaveIndex();
        int numOfWaves = SpawnManager.instance.GetNumberOfWaves();
        if (waveIndex + 1 >= numOfWaves)
        {
            wavesText.text = "Map completed!";
        }
        else
        {
            wavesText.text = waveIndex + " / " + numOfWaves;
        }

        moneyText.text = "Testing from code: 123";
    }
}
