using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] Toggle vSyncToggle;

    public void OpenSettingsPanel()
    {
        RefreshSettingsPanel(SettingsManager.GetSettingsData());
        gameObject.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        Serializer.Serialize();
        gameObject.SetActive(false);
    }

    public void RefreshSettingsPanel(SettingsData settingsData)
    {
        qualityDropdown.value = settingsData.qualityLevel;
        vSyncToggle.isOn = settingsData.vSync;
    }

    public void SetQualitySettingsFromDropdown(TMP_Dropdown dropdown)
    {
        SettingsManager.SetQualitySettingsWithoutVSync(dropdown.value);
    }

    public void SetVSync(Toggle toggle)
    {
        SettingsManager.SetVSync(toggle.isOn);
    }
}
