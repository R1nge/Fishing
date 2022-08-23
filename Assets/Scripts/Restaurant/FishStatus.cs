using UnityEngine;

namespace Restaurant
{
    public class FishStatus : MonoBehaviour
    {
        private CookingManager _cookingManager;
        private void Awake() => _cookingManager = FindObjectOfType<CookingManager>();
        public bool IsWashed { get; private set; }
        public bool IsChopped { get; private set; }
        public bool IsCooked { get; private set; }
        public bool IsSalted { get; private set; }
        public bool IsPeppered { get; private set; }

        public void Wash()
        {
            if (!HasFish()) return;
            IsWashed = true;
            if (!IsCooked)
            {
                IsSalted = false;
                IsPeppered = false;
            }
        }

        public void Chop()
        {
            if (!HasFish()) return;
            IsChopped = true;
        }

        public void Cook()
        {
            if (!HasFish()) return;
            IsCooked = true;
        }

        public void Salt()
        {
            if (!HasFish()) return;
            IsSalted = true;
        }

        public void Pepper()
        {
            if (!HasFish()) return;
            IsPeppered = true;
        }

        public void ResetStatus()
        {
            IsWashed = false;
            IsChopped = false;
            IsCooked = false;
            IsSalted = false;
            IsPeppered = false;
        }

        private bool HasFish()
        {
            if (_cookingManager.CurrentFish != null) return true;
            Debug.LogWarning("Fish not selected", this);
            return false;
        }
    }
}