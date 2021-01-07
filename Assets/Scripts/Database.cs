using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatabaseSerializationData
{
    public List<string> purchased;
}

public class Database : MonoBehaviour
{
    //List of purchased items, stored as IDs
    List<string> purchased = new List<string>();

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
        }
    }

    public void AddPurchaseElementID(string ID)
    {
        if (!purchased.Contains(ID))
        {
            purchased.Add(ID);
            Serializer.Serialize();
        }
        else
        {
            Debug.LogError($"Can't add - Item of ID {ID}, is in database!");
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
            purchased = purchased
        };
        return data;
    }

    public void SetDatabaseFromData(DatabaseSerializationData databaseSerializationData)
    {
        purchased = databaseSerializationData.purchased;
    }
}
