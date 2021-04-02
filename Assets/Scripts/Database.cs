using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatabaseSerializationData
{
    public List<string> purchasedID;
    public List<UpgradeKitDataSerizalizationData> upgrades;

    public string weaponDataID;
    public string weaponUpgradeKitDataID;

    public string characterDataID;

    public string mapDataID;
}

public class Database : MonoBehaviour
{
    //Lists of assets IDs loaded from Resource
    CharacterData[] availableCharacters;
    MapData[] availableMaps;
    WeaponData[] availableWeapons;
    UpgradeData[] availableUpgrades;

    //List of purchased items, stored as IDs
    List<string> purchased = new List<string>();
    List<UpgradeKitData> upgrades = new List<UpgradeKitData>();

    WeaponData weaponData = null;

    UpgradeKitData weaponUpgradeKitData = null;

    CharacterData characterData;

    MapData mapData = null;


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
            LoadResources();
            DontDestroyOnLoad(gameObject);
        }
    }

    void LoadResources()
    {
        availableCharacters = Resources.LoadAll<CharacterData>("Characters");
        availableMaps = Resources.LoadAll<MapData>("Maps");
        availableWeapons = Resources.LoadAll<WeaponData>("Weapons");
        availableUpgrades = Resources.LoadAll<UpgradeData>("Upgrades");
    }

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public void SetWeaponData(WeaponData newWeaponData)
    {
        weaponData = newWeaponData;
    }

    public UpgradeKitData GetWeaponUpgradeKitData()
    {
        return weaponUpgradeKitData;
    }

    public void SetWeaponUpgradeKitData(UpgradeKitData upgradeKitData)
    {
        weaponUpgradeKitData = upgradeKitData;
    }

    public void SetCharacterData(CharacterData _characterData)
    {
        characterData = _characterData;
    }

    public CharacterData GetCharacterData()
    {
        return characterData;
    }

    public void SetMapData(MapData _mapData)
    {
        mapData = _mapData;
    }

    public MapData GetMapData()
    {
        return mapData;
    }

    public void AddPurchaseElementID(PurchaseData purchaseData)
    {
        if (!purchased.Contains(purchaseData.GetID))
        {
            purchased.Add(purchaseData.GetID);
            //Creating copy of upgradeKit to prevent from changing values directly on ScriptableObject
            upgrades.Add(purchaseData.UpgradeKit.MakeCopy());
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


    WeaponData GetWeaponDataByID(string ID)
    {
        foreach (WeaponData data in availableWeapons)
        {
            if (data.GetID == ID)
                return data;
        }

        return null;
    }

    public UpgradeData GetUpgradeDataByID(string ID)
    {
        foreach (UpgradeData data in availableUpgrades)
        {
            if (data.GetID == ID)
                return data;
        }

        return null;
    }

    public UpgradeKitData GetUpgradeKitDataByID(string ID)
    {
        foreach (UpgradeKitData data in upgrades)
        {
            if (data.ID == ID)
                return data;
        }

        return null;
    }

    public UpgradeKitData CreateUpgradeKitDataFromSerialized(UpgradeKitDataSerizalizationData upgradeKitDataSerizalizationData)
    {
        UpgradeKitData upgradeKitData = new UpgradeKitData(upgradeKitDataSerizalizationData);

        return upgradeKitData;
    }

    List<UpgradeKitDataSerizalizationData> CreateSerializedUpgradeData()
    {
        List<UpgradeKitDataSerizalizationData> serialized = new List<UpgradeKitDataSerizalizationData>();
        foreach (UpgradeKitData item in upgrades)
        {
            serialized.Add(item.GetSerializationData());
        }

        return serialized;
    }

    CharacterData GetCharacterDataByID(string ID)
    {
        foreach (CharacterData data in availableCharacters)
        {
            if (data.GetID == ID)
                return data;
        }

        return null;
    }

    MapData GetMapDataByID(string ID)
    {
        foreach (MapData data in availableMaps)
        {
            if (data.GetID == ID)
                return data;
        }

        return null;
    }

    public DatabaseSerializationData GetSerializationData()
    {
        string weaponID = weaponData ? weaponData.GetID : "";
        string weaponUpgradeKitID = weaponUpgradeKitData != null? weaponUpgradeKitData.ID : "";

        string characterDataID = characterData ? characterData.GetID : "";
        string mapDataID = mapData ? mapData.GetID : "";

        DatabaseSerializationData data = new DatabaseSerializationData
        {
            purchasedID = new List<string>(purchased),
            upgrades = CreateSerializedUpgradeData(),
            weaponDataID = weaponID,
            weaponUpgradeKitDataID = weaponUpgradeKitID,
            characterDataID = characterDataID,
            mapDataID = mapDataID
        };
        return data;
    }

    public void SetDatabaseFromData(DatabaseSerializationData databaseSerializationData)
    {
        purchased = databaseSerializationData.purchasedID;
        upgrades = new List<UpgradeKitData>();
        foreach (UpgradeKitDataSerizalizationData item in databaseSerializationData.upgrades)
        {
            upgrades.Add(CreateUpgradeKitDataFromSerialized(item));
        }

        weaponData = GetWeaponDataByID(databaseSerializationData.weaponDataID);
        weaponUpgradeKitData = GetUpgradeKitDataByID(databaseSerializationData.weaponUpgradeKitDataID);

        characterData = GetCharacterDataByID(databaseSerializationData.characterDataID);

        mapData = GetMapDataByID(databaseSerializationData.mapDataID);
    }
}
