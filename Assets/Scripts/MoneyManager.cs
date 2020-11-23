using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    Statistic gold = new Statistic(0, 50, 1000000);

    public static MoneyManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Removed MoneyManager there is one already set!");
        }
    }

    public int GetGoldAmount()
    {
        return gold.GetAmount();
    }
}
