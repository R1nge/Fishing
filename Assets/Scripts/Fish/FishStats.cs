using UnityEngine;

namespace Fish
{
    [CreateAssetMenu(fileName = "FishStats", menuName = "SO/Fish")]
    public class FishStats : ScriptableObject
    {
        [SerializeField] private int weight;
        [SerializeField] private int pricePerWeight;
        [SerializeField] private int spawnDepth;
        public int TotalPrice => weight * pricePerWeight * spawnDepth;
    }
}