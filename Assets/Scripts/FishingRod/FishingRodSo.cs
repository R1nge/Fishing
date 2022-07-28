using System;
using UnityEngine;

namespace FishingRod
{
    [CreateAssetMenu(fileName = "FishingRod", menuName = "SO/FishingRod")]
    public class FishingRodSo : ScriptableObject
    {
        public Sprite icon;
        public RodData data;

        public event Action OnSpriteChanged;
        public event Action OnStatsChanged;

        public void SetAll(FishingRodSo so)
        {
            icon = so.icon;
            data.biteSprite = so.data.biteSprite;
            data.rodSprite = so.data.rodSprite;
            data.hookSprite = so.data.hookSprite;

            data.maxWeight = so.data.maxWeight;
            data.maxLength = so.data.maxLength;

            data.verticalSpeed = so.data.verticalSpeed;
            data.horizontalSpeed = so.data.horizontalSpeed;

            OnSpriteChanged?.Invoke();
            OnStatsChanged?.Invoke();
        }
    }

    [Serializable]
    public class RodData
    {
        public string rodSprite;
        public string hookSprite;
        public string biteSprite;
        public int maxWeight;
        public int maxLength;
        public float verticalSpeed, horizontalSpeed;
    }
}