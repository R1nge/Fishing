using Fish;
using UnityEngine;

namespace Restaurant
{
    public class CookingManager : MonoBehaviour
    {
        private FishStatus _fishStatus;

        private void Awake() => _fishStatus = FindObjectOfType<FishStatus>();

        public FishSO CurrentFish { get; private set; }

        public void SelectFish(FishSO fish)
        {
            if (fish.data.amount <= 0)
            {
                Debug.LogWarning("Fish amount is <= 0");
                return;
            }

            _fishStatus.ResetStatus();
            CurrentFish = fish;
        }

        public void Delete() => CurrentFish = null;
    }
}