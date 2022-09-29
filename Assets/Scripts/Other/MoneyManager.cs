using System;
using System.Collections;
using BayatGames.SaveGameFree;
using UnityEngine;

namespace Other
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private int money;
        public int Money => money;

        private const string MONEY = "money";
        public event Action<int> OnMoneyAmountChanged;
        private SaveGameWeb _web;

        private void Awake()
        {
            if (SaveGame.Exists(MONEY))
                Load();
            else
                Save();
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

        public void CloudSave() => StartCoroutine(CloudLoad_c());

        private IEnumerator CloudSave_c()
        {
            print("Saved money");
            yield break;
        }

        public void CloudLoad() => StartCoroutine(CloudLoad_c());

        private IEnumerator CloudLoad_c()
        {
            print("Money saved");
            yield break;
        }

        private void Load()
        {
            money = SaveGame.Load(MONEY, 200);
            OnMoneyAmountChanged?.Invoke(money);
        }
    }
}