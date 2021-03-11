using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatabaseSerializationData
{
    public List<string> purchasedID;
    public List<UpgradeKitDataSerizalizationData> upgrades;

    public string firstWeaponDataID;
    public string secondWeaponDataID;

    public string firstWeaponUpgradeKitDataID;
    public string secondWeaponUpgradeKitDataID;

    public string characterDataID;
}

public class Database : MonoBehaviour
{
    //Lists of assets IDs loaded from Resource
    CharacterData[] availableCharacters;
    PurchaseData[] availableMaps;
    WeaponData[] availableWeapons;
    UpgradeData[] availableUpgrades;

    //List of purchased items, stored as IDs
    List<string> purchased = new List<string>();
    List<UpgradeKitData> upgrades = new List<UpgradeKitData>();

    WeaponData firstWeaponData = null;
    WeaponData secondWeaponData = null;

    UpgradeKitData firstWeaponUpgradeKitData = null;
    UpgradeKitData secondWeaponUpgradeKitData = null;

    CharacterData characterData;


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
        availableMaps = Resources.LoadAll<PurchaseData>("Maps");
        availableWeapons = Resources.LoadAll<WeaponData>("Weapons");
        availableUpgrades = Resources.LoadAll<UpgradeData>("Upgrades");
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

    public void SetCharacterData(CharacterData _characterData)
    {
        characterData = _characterData;
    }

    public CharacterData GetCharacterData()
    {
        return characterData;
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

    PurchaseData GetMapDataByID(string ID)
    {
        foreach (PurchaseData data in availableMaps)
        {
            if (data.GetID == ID)
                return data;
        }

        return null;
    }

    public DatabaseSerializationData GetSerializationData()
    {
        string firstWeaponID = firstWeaponData ? firstWeaponData.GetID : "";
        string secondWeaponID = secondWeaponData ? secondWeaponData.GetID : "";

        string firstWeaponUpgradeKitID = firstWeaponUpgradeKitData != null? firstWeaponUpgradeKitData.ID : "";
        string secondWeaponUpgradeKitID = secondWeaponUpgradeKitData != null? secondWeaponUpgradeKitData.ID : "";

        string characterDataID = characterData ? characterData.GetID : "";

        DatabaseSerializationData data = new DatabaseSerializationData
        {
            purchasedID = new List<string>(purchased),
            upgrades = CreateSerializedUpgradeData(),
            firstWeaponDataID = firstWeaponID,
            secondWeaponDataID = secondWeaponID,
            firstWeaponUpgradeKitDataID = firstWeaponUpgradeKitID,
            secondWeaponUpgradeKitDataID = secondWeaponUpgradeKitID,
            characterDataID = characterDataID
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

        firstWeaponData = GetWeaponDataByID(databaseSerializationData.firstWeaponDataID);
        secondWeaponData = GetWeaponDataByID(databaseSerializationData.secondWeaponDataID);

        firstWeaponUpgradeKitData = GetUpgradeKitDataByID(databaseSerializationData.firstWeaponUpgradeKitDataID);
        secondWeaponUpgradeKitData = GetUpgradeKitDataByID(databaseSerializationData.secondWeaponUpgradeKitDataID);

        characterData = GetCharacterDataByID(databaseSerializationData.characterDataID);
    }
}
