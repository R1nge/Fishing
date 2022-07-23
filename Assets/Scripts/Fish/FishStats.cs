using UnityEngine;

namespace Fish
{
    [CreateAssetMenu(fileName = "FishStats", menuName = "SO/Fish")]
    public class FishStats : ScriptableObject
    {
        public int weight;
        public int pricePerWeight;
        public int spawnDepth;
        public int TotalPrice => weight * pricePerWeight * spawnDepth;
    }
}