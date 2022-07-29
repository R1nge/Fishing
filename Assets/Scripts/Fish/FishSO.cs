using System;
using UnityEngine;

namespace Fish
{
    [CreateAssetMenu(fileName = "FishData", menuName = "SO/Fish")]
    public class FishSO : ScriptableObject
    {
        public Sprite icon;
        public string fishName;
        public FishData data;
        [SerializeField] private int weight;
        [SerializeField] private int pricePerWeight;
        [SerializeField] private int rareModifier;
        public int TotalPrice => weight * pricePerWeight * rareModifier;
    }

    [Serializable]
    public class FishData
    {
        public int amount;
    }
}