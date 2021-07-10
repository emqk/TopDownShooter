
[System.Serializable]
public class MoneySerializationData
{
    public Statistic gold;
}

public static class MoneyManager
{
    static Statistic gold = new Statistic(0, 20, int.MaxValue);

    public static int GetGoldAmount()
    {
        return gold.GetAmount();
    }

    /// <summary></summary>
    /// <param name="cost">Must be >= 0</param>
    /// <returns></returns>
    public static bool HaveEnoughGold(int cost)
    {
        return gold.GetAmount() - cost >= 0;
    }

    public static void AddGold(int amount)
    {
        gold.ChangeByAmount(amount);
        MainMenuUIManager.instance.RefreshGold();
    }
    
    public static void SpendGold(int cost)
    {
        gold.ChangeByAmount(-cost);
        MainMenuUIManager.instance.RefreshGold();
    }

    public static MoneySerializationData GetMoneyData()
    {
        MoneySerializationData data = new MoneySerializationData
        {
            gold = gold
        };
        return data;
    }

    public static void SetMoneyFromData(MoneySerializationData moneySerializationData)
    {
        gold = moneySerializationData.gold;
    }
}
