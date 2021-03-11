using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    None, Rate, Damage, Speed
}

[System.Serializable]
public class UpgradeKitDataSerizalizationData
{
    public string upgradeKitID;
    public List<UpgradeDataInstanceSerializationData> upgradeDatas = new List<UpgradeDataInstanceSerializationData>();
}

[System.Serializable]
public class UpgradeDataInstanceSerializationData
{
    public string upgradeDataID;
    public int currentLevel;
}


[System.Serializable]
public class UpgradeKitData
{
    public string ID;
    public List<UpgradeDataInstance> upgradeDatas = new List<UpgradeDataInstance>();

    public UpgradeKitData()
    {

    }

    public UpgradeKitData(UpgradeKitDataSerizalizationData upgradeKitDataSerizalizationData)
    {
        ID = upgradeKitDataSerizalizationData.upgradeKitID;
        foreach (UpgradeDataInstanceSerializationData item in upgradeKitDataSerizalizationData.upgradeDatas)
        {
            UpgradeDataInstance upgradeData = new UpgradeDataInstance(Database.instance.GetUpgradeDataByID(item.upgradeDataID), item.currentLevel);
            upgradeDatas.Add(upgradeData);
        }
    }

    public UpgradeDataInstance GetUpgradeDataInstanceByType(UpgradeType upgradeType)
    {
        foreach (UpgradeDataInstance item in upgradeDatas)
        {
            if (item.UpgradeType == upgradeType)
            {
                return item;
            }
        }

        Debug.LogError("Can't find upgrade data of given type!");
        return null;
    }

    public UpgradeKitDataSerizalizationData GetSerializationData()
    {
        UpgradeKitDataSerizalizationData serializationData = new UpgradeKitDataSerizalizationData();
        serializationData.upgradeKitID = ID;
        foreach (UpgradeDataInstance upgradeInstance in upgradeDatas)
        {
            UpgradeDataInstanceSerializationData instanceToSerialize = new UpgradeDataInstanceSerializationData
            {
                upgradeDataID = upgradeInstance.GetID,
                currentLevel = upgradeInstance.CurrentLevel
            };
            serializationData.upgradeDatas.Add(instanceToSerialize);
        }

        return serializationData;
    }

    public UpgradeKitData MakeCopy()
    {
        UpgradeKitData upgradeKitDataCopy = new UpgradeKitData();
        upgradeKitDataCopy.ID = ID;
        upgradeKitDataCopy.upgradeDatas = new List<UpgradeDataInstance>();
        foreach (UpgradeDataInstance item in upgradeDatas)
        {
            upgradeKitDataCopy.upgradeDatas.Add(item.MakeCopy());
        }

        return upgradeKitDataCopy;
    }
}

[System.Serializable]
public class UpgradeDataInstance
{
    [SerializeField] UpgradeData upgradeData;
    [SerializeField] int currentLevel;

    public UpgradeDataInstance()
    { 
        currentLevel = -1;
    }

    public UpgradeDataInstance(UpgradeData myUpgradeData, int myCurrentLevel)
    {
        upgradeData = myUpgradeData;
        currentLevel = myCurrentLevel;
    }

    public string GetID { get => upgradeData.GetID; }
    public string UpgradeName { get => upgradeData.UpgradeName; }
    public UpgradeType UpgradeType { get => upgradeData.UpgradeType; }
    public Sprite Thumbnail { get => upgradeData.Thumbnail; }
    public List<PowerAndCostPair> PowerAndCost { get => upgradeData.PowerAndCost; }
    public int MaxLevels { get => upgradeData.MaxLevels; }

    public int CurrentLevel { get => currentLevel; }


    public bool Upgrade()
    {
        if (!CanBeUpgraded())
            return false;

        currentLevel++;
        return true;
    }

    public bool CanBeUpgraded()
    {
        return CurrentLevel < upgradeData.PowerAndCost.Count - 1;
    }

    /// <summary> Is current level >= 0? </summary>
    /// <returns></returns>
    public bool WasUpgraded()
    {
        return currentLevel >= 0;
    }

    public PowerAndCostPair GetCurrentPowerCostPair()
    {
        if (currentLevel < 0)
            return null;

        return upgradeData.PowerAndCost[CurrentLevel];
    }

    public PowerAndCostPair GetNextPowerCostPair()
    {
        if (currentLevel + 1 >= upgradeData.PowerAndCost.Count)
            return null;

        return upgradeData.PowerAndCost[CurrentLevel + 1];
    }

    public UpgradeDataInstance MakeCopy()
    {
        UpgradeDataInstance upgradeDataInstanceCopy = new UpgradeDataInstance();
        upgradeDataInstanceCopy.upgradeData = upgradeData;
        upgradeDataInstanceCopy.currentLevel = currentLevel;

        return upgradeDataInstanceCopy;
    }
}

[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "Weapons/Create New Upgrade Data")]
[System.Serializable]
public class UpgradeData : ScriptableObject
{
    [SerializeField] string ID;
    [SerializeField] string upgradeName;
    [SerializeField] UpgradeType upgradeType;
    [SerializeField] Sprite thumbnail;

    [SerializeField] List<PowerAndCostPair> powerAndCost;

    public string GetID { get => ID; }
    public string UpgradeName { get => upgradeName; }
    public UpgradeType UpgradeType { get => upgradeType; }
    public Sprite Thumbnail { get => thumbnail; }
    public List<PowerAndCostPair> PowerAndCost { get => powerAndCost; }
    public int MaxLevels { get => PowerAndCost.Count; }
}

[System.Serializable]
public class PowerAndCostPair
{
    public float power;
    public int cost;
}
