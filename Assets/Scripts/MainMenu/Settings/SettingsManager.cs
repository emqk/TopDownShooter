using UnityEngine;

public static class SettingsManager
{
    public static SettingsData GetSettingsData()
    {
        SettingsData data = new SettingsData
        {
            qualityLevel = QualitySettings.GetQualityLevel(),
            vSync = QualitySettings.vSyncCount == 1 ? true : false
        };
        return data;
    }

    public static void SetSettingsFromData(SettingsData settingsData)
    {
        SetQualitySettingsWithoutVSync(settingsData.qualityLevel);
        SetVSync(settingsData.vSync);
    }

    public static void SetQualitySettingsWithoutVSync(int index)
    {
        bool vSyncBefore = QualitySettings.vSyncCount == 1 ? true : false;
        QualitySettings.SetQualityLevel(index);
        SetVSync(vSyncBefore);
    }

    public static void SetVSync(bool enable)
    {
        if (enable)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}
