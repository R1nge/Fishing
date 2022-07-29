using System;
using UnityEngine;

namespace FishingRod
{
    [CreateAssetMenu(fileName = "FishingRod", menuName = "SO/FishingRod")]
    public class FishingRodSo : ScriptableObject
    {
        public Sprite icon;
        public int price;
        public RodData data;

        public event Action OnSpriteChanged;
        public event Action OnStatsChanged;
        public event Action OnStatusChanged;

        public void SetAll(FishingRodSo so)
        {
            icon = so.icon;
            data.rodTitle = so.data.rodTitle;
            data.isUnlocked = so.data.isUnlocked;
            data.biteSprite = so.data.biteSprite;
            data.rodSprite = so.data.rodSprite;
            data.hookSprite = so.data.hookSprite;

            data.maxWeight = so.data.maxWeight;
            data.maxLength = so.data.maxLength;

            data.verticalSpeed = so.data.verticalSpeed;
            data.horizontalSpeed = so.data.horizontalSpeed;

            data.isUnlocked = so.data.isUnlocked;

            OnSpriteChanged?.Invoke();
            OnStatsChanged?.Invoke();
            OnStatusChanged?.Invoke();
        }

        public void Unlock(FishingRodSo so)
        {
            if (so.data.isUnlocked) return;
            so.data.isUnlocked = true;
            OnStatusChanged?.Invoke();
        }
    }

    [Serializable]
    public class RodData
    {
        public string rodTitle;
        public string rodSprite;
        public string hookSprite;
        public string biteSprite;
        public int maxWeight;
        public int maxLength;
        public float verticalSpeed, horizontalSpeed;
        public bool isUnlocked;
    }
}