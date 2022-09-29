using System;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "Tables", menuName = "Restaurant/Tables")]
    public class Tables : ScriptableObject
    {
        public TablesData data;
    }
    
    [Serializable]
    public class TablesData
    {
        public int freeAmount;
        public int amount;
        public event Action<int, int> OnValueChanged;

        public void Update() => OnValueChanged?.Invoke(freeAmount, amount);
    }
}