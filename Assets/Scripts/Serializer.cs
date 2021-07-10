using System.IO;
using UnityEngine;

[System.Serializable]
public class SerializeData
{
    public DatabaseSerializationData databaseData;
    public MoneySerializationData moneyData;
    public SettingsData settingsData;
}

public static class Serializer
{
    static string filePath = Application.persistentDataPath + "/Saves";
    static string fileName = "saveData.json";
    static string fullPath = filePath + Path.DirectorySeparatorChar + fileName;

    public static void Serialize()
    {
        Directory.CreateDirectory(filePath);
        SerializeData dataToSerialize = new SerializeData
        {
            databaseData = Database.instance.GetSerializationData(),
            moneyData = MoneyManager.GetMoneyData(),
            settingsData = SettingsManager.GetSettingsData()
        };
        string json = JsonUtility.ToJson(dataToSerialize);
        File.WriteAllText(fullPath, json);
        Debug.Log("Data has been serialized to: " + fullPath);
    }

    public static void Load()
    {
        SerializeData serializeData = Deserialize();
        if (serializeData != null)
        {
            Database.instance.SetDatabaseFromData(serializeData.databaseData);
            MoneyManager.SetMoneyFromData(serializeData.moneyData);
            SettingsManager.SetSettingsFromData(serializeData.settingsData);
        }
    }

    static SerializeData Deserialize()
    {
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            SerializeData deserializedData = JsonUtility.FromJson<SerializeData>(json);
            Debug.Log("Data has been loaded from: " + fullPath);
            return deserializedData;
        }

        Debug.LogError("Can't find file at: " + fullPath);
        return null;
    }

    public static bool SaveFileExist()
    {
        return File.Exists(fullPath);
    }
}
