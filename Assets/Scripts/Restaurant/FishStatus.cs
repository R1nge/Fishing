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

        public void Wash()
        {
            if (_cookingManager.CurrentFish == null)
            {
                Debug.LogWarning("Fish not selected", this);
                return;
            }

            IsWashed = true;
        }

        public void Chop()
        {
            if (_cookingManager.CurrentFish == null)
            {
                Debug.LogWarning("Fish not selected", this);
                return;
            }

            IsChopped = true;
        }

        public void Cook()
        {
            if (_cookingManager.CurrentFish == null)
            {
                Debug.LogWarning("Fish not selected", this);
                return;
            }

            IsCooked = true;
        }

        public void ResetStatus()
        {
            IsWashed = false;
            IsChopped = false;
            IsCooked = false;
        }
    }
}