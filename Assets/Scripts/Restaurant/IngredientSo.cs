using System;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "Cooking Ingredient", menuName = "Restaurant/Cooking Ingredient")]
    public class IngredientSo : ScriptableObject
    {
        public string title;
        public Sprite icon;
        public GameObject prefab;
        public Data data;
        public event Action<int> OnAmountChanged;

        public void Increase(int amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Trying to add negative number", this);
                return;
            }

            data.amount += amount;
            OnAmountChanged?.Invoke(data.amount);
        }

        public void Decrease(int amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Trying to subtract negative number", this);
                return;
            }
            
            data.amount -= amount;
            OnAmountChanged?.Invoke(data.amount);
        }
    }

    [Serializable]
    public class Data
    {
        public int amount;
    }
}