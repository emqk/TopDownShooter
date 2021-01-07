using System.Collections.Generic;
using UnityEngine;

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
}

[System.Serializable]
public class UpgradeData
{
    public string upgradeName;
    public Sprite thumbnail;

    public List<PowerAndCostPair> powerAndCost;

    int currentLevel = 0;
    public int maxLevels;

    public int CurrentLevel { get => currentLevel; }

    //Copy constructor
    public UpgradeData(UpgradeData other)
    {
        upgradeName = other.upgradeName;
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

    public PowerAndCostPair GetCurrentPowerCostPair()
    {
        return powerAndCost[CurrentLevel];
    }
}

[System.Serializable]
public class PowerAndCostPair
{
    public float power;
    public int cost;
}
