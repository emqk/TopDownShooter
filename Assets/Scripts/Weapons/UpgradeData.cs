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
    public List<UpgradeDataInstance> upgradeDatas;


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
}

[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "Weapons/Create New Upgrade Data")]
[System.Serializable]
public class UpgradeData : ScriptableObject
{
    [SerializeField] string upgradeName;
    [SerializeField] UpgradeType upgradeType;
    [SerializeField] Sprite thumbnail;

    [SerializeField] List<PowerAndCostPair> powerAndCost;

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
