using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    //List of purchased items, stored as IDs
    [SerializeField] List<string> purchased = new List<string>();

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
        Debug.Log($"Element of ID: {ID} wasn't purchased");
        return false;
    }
}
