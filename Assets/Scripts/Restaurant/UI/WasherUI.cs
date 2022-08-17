using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.UI
{
    public class WasherUI : MonoBehaviour
    {
        [SerializeField] private GameObject userInterface;
        [SerializeField] private Image fish;
        private CookingManager _cookingManager;

        private void Awake()
        {
            _cookingManager = FindObjectOfType<CookingManager>();
            Close();
        }

        public void Open()
        {
            if (_cookingManager.CurrentFish == null)
            {
                Debug.LogWarning("Fish is not selected");
                return;
            }

            fish.sprite = _cookingManager.CurrentFish.icon;
            userInterface.SetActive(true);
        }

        public void Close() => userInterface.SetActive(false);
    }
}