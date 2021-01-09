using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    None, Rate, Damage, Speed
}

[System.Serializable]
public class UpgradeKitData
{
    public string ID;
    public List<UpgradeData> upgradeDatas;

    //Copy contructor
    public UpgradeKitData(UpgradeKitData other)
    {
        ID = other.ID;
        upgradeDatas = new List<UpgradeData>(other.upgradeDatas);
        upgradeDatas = other.upgradeDatas.ConvertAll(book => new UpgradeData(book));
    }

    public UpgradeData GetUpgradeDataByType(UpgradeType upgradeType)
    {
        foreach (UpgradeData item in upgradeDatas)
        {
            if (item.upgradeType == upgradeType)
            {
                return item;
            }
        }

        Debug.LogError("Can't find upgrade data of given type!");
        return null;
    }
}

[System.Serializable]
public class UpgradeData
{
    public string upgradeName;
    public UpgradeType upgradeType;
    public Sprite thumbnail;

    public List<PowerAndCostPair> powerAndCost;

    [SerializeField] int currentLevel = -1;
    public int maxLevels;

    public int CurrentLevel { get => currentLevel; }

    public UpgradeData()
    {
        currentLevel = -1;
    }

    //Copy constructor
    public UpgradeData(UpgradeData other)
    {
        upgradeName = other.upgradeName;
        upgradeType = other.upgradeType;
        thumbnail = other.thumbnail;
        powerAndCost = other.powerAndCost;
        currentLevel = other.currentLevel;
        maxLevels = other.maxLevels;
    }

    public bool Upgrade()
    {
        if (!CanBeUpgraded())
            return false;

        currentLevel++;
        return true;
    }

    public bool CanBeUpgraded()
    {
        return CurrentLevel < powerAndCost.Count - 1;
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

        return powerAndCost[CurrentLevel];
    }

    public PowerAndCostPair GetNextPowerCostPair()
    {
        if (currentLevel + 1 >= powerAndCost.Count)
            return null;

        return powerAndCost[CurrentLevel + 1];
    }
}

[System.Serializable]
public class PowerAndCostPair
{
    public float power;
    public int cost;
}
