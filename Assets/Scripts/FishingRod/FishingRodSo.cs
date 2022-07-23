using System;
using UnityEngine;

namespace FishingRod
{
    [CreateAssetMenu(fileName = "FishingRod", menuName = "SO/FishingRod")]
    public class FishingRodSo : ScriptableObject
    {
        public RodData data;

        public event Action OnSpriteChanged;
        public event Action OnStatsChanged;

        private void OnEnable() => SetAll(this);

        public void SetAll(FishingRodSo so)
        {
            data.biteImage = so.data.biteImage;
            data.biteSprite = so.data.biteSprite;

            data.rodImage = so.data.rodImage;
            data.rodSprite = so.data.rodSprite;

            data.hookImage = so.data.hookImage;
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
        public string biteImage, biteSprite;
        public string rodImage, rodSprite;
        public string hookImage, hookSprite;
        public int maxWeight;
        public int maxLength;
        public float verticalSpeed, horizontalSpeed;
    }
}