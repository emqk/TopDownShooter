using UnityEngine;

[System.Serializable]
public class Statistic
{
    [SerializeField] int maxAmount = 100;
    [SerializeField] int currentAmount = 100;
    [SerializeField] int minAmount = 0;

    public Statistic()
    {}

    public Statistic(int _minAmount, int _amount, int _maxAmount)
    {
        minAmount = _minAmount;
        currentAmount = _amount;
        maxAmount = _maxAmount;
    }

    public int GetAmount()
    {
        return currentAmount;
    }

    public float GetAmountNormalized()
    {
        return (float)currentAmount / (float)maxAmount;
    }

    public int GetMaxAmount()
    {
        return maxAmount;
    }
    public int GetMinAmount()
    {
        return minAmount;
    }

    public bool IsGreaterThanMinimum()
    {
        return GetAmount() > minAmount;
    }

    public void ChangeByAmount(int amount) 
    {
        currentAmount = UnityEngine.Mathf.Clamp(currentAmount + amount, minAmount, maxAmount);
    }
}