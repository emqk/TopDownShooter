using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatabaseSerializationData
{
    public List<string> purchased;
    public List<UpgradeKitData> upgrades;
}

public class Database : MonoBehaviour
{
    //List of purchased items, stored as IDs
    List<string> purchased = new List<string>();
    List<UpgradeKitData> upgrades = new List<UpgradeKitData>();

    WeaponData firstWeaponData = null;
    WeaponData secondWeaponData = null;

    UpgradeKitData firstWeaponUpgradeKitData = null;
    UpgradeKitData secondWeaponUpgradeKitData = null;


    public static Database instance;

    private void Awake()
    {
        if (instance)
        {
            Debug.Log("One database is already on this scene, Destroying!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public WeaponData GetWeaponData(bool isItFirst)
    {
        if (isItFirst)
            return firstWeaponData;
        else
            return secondWeaponData;
    }

    public void SetWeaponData(WeaponData weaponData, bool isItFirst)
    {
        if (isItFirst)
            firstWeaponData = weaponData;
        else
            secondWeaponData = weaponData;
    }

    public UpgradeKitData GetWeaponUpgradeKitData(bool isItFirstWeapon)
    {
        if (isItFirstWeapon)
            return firstWeaponUpgradeKitData;
        else
            return secondWeaponUpgradeKitData;
    }

    public void SetWeaponUpgradeKitData(UpgradeKitData upgradeKitData, bool isItFirst)
    {
        if (isItFirst)
            firstWeaponUpgradeKitData = upgradeKitData;
        else
            secondWeaponUpgradeKitData = upgradeKitData;
    }

    public UpgradeKitData GetUpgradeKitData(string kitID)
    {
        foreach (UpgradeKitData currKit in upgrades)
        {
            if (currKit.ID == kitID)
            {
                return currKit;
            }
        }

        Debug.LogError("Can't find Upgrade data of id: " + kitID);
        return null;
    }


    public void AddPurchaseElementID(PurchaseData purchaseData)
    {
        if (!purchased.Contains(purchaseData.GetID))
        {
            purchased.Add(purchaseData.GetID);
            upgrades.Add(purchaseData.UpgradeKit);
            Serializer.Serialize();
        }
        else
        {
            Debug.LogError($"Can't add - Item of ID {purchaseData.GetID}, is in database!");
        }
    }

    public bool IsElementOfIDPurchased(string ID)
    {
        if (purchased.Contains(ID))
        {
            return true;
        }
        return false;
    }

    public DatabaseSerializationData GetSerializationData()
    {
        DatabaseSerializationData data = new DatabaseSerializationData
        {
            purchased = purchased,
            upgrades = upgrades
        };
        return data;
    }

    public void SetDatabaseFromData(DatabaseSerializationData databaseSerializationData)
    {
        purchased = databaseSerializationData.purchased;
        upgrades = databaseSerializationData.upgrades;
    }
}
