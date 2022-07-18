using UnityEngine;

namespace FishingRod
{
    [CreateAssetMenu(fileName = "FishingRodStats", menuName = "SO/FishingRod/Stats")]
    public class FishingRodStats : ScriptableObject
    {
        public int maxLength;
        public int speed;
    }
}