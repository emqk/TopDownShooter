using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeKitData
{
    public string ID;
    public List<UpgradeData> upgradeDatas;
}

[System.Serializable]
public class UpgradeData
{
    public string upgradeName;
    public Sprite thumbnail;

    public List<PowerAndCostPair> powerAndCost;

    public int currentLevel;
    public int maxLevels;
}

[System.Serializable]
public class PowerAndCostPair
{
    public float power;
    public int cost;
}
