using System;
using BayatGames.SaveGameFree;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int money;

    public int Money => money;

    private const string MONEY = "Money";
    public event Action<int> OnMoneyAmountChanged;

    private void Awake()
    {
        if (SaveGame.Exists(MONEY))
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    private void Start() => OnMoneyAmountChanged?.Invoke(money);

    public bool Spend(int amount)
    {
        if (money - amount >= 0)
        {
            money -= amount;
            OnMoneyAmountChanged?.Invoke(money);
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
        OnMoneyAmountChanged?.Invoke(money);
        Save();
    }

    private void Save()
    {
        OnMoneyAmountChanged?.Invoke(money);
        SaveGame.Save(MONEY, money);
    }

    private void Load()
    {
        money = SaveGame.Load(MONEY, 200);
        OnMoneyAmountChanged?.Invoke(money);
    }
}