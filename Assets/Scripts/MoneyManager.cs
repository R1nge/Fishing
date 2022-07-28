using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int money;
    private const string MONEY = "Money";
    public event Action<int> OnMoneyAmountChanged;

    private void Start() => Load();

    public bool Spend(int amount)
    {
        if (money - amount >= 0)
        {
            money -= amount;
            Save();
            return true;
        }

        return false;
    }

    public void Earn(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Trying to add negative value");
            return;
        }

        money += amount;
        Save();
    }

    private void Save()
    {
        OnMoneyAmountChanged?.Invoke(money);
        PlayerPrefs.SetInt(MONEY, money);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        money = PlayerPrefs.GetInt(MONEY);
        OnMoneyAmountChanged?.Invoke(money);
    }
}