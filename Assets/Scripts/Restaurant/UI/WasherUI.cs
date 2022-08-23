using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.UI
{
    public class WasherUI : MonoBehaviour
    {
        [SerializeField] private GameObject userInterface;
        [SerializeField] private Image fish;
        [SerializeField] private TextMeshProUGUI progress;
        private CookingManager _cookingManager;
        private FishStatus _fishStatus;

        private void Awake()
        {
            _cookingManager = FindObjectOfType<CookingManager>();
            _fishStatus = FindObjectOfType<FishStatus>();
            UpdateProgressUI(0);
            Close();
        }

        public void Open()
        {
            if (_cookingManager.CurrentFish == null)
            {
                Debug.LogWarning("Fish is not selected");
                return;
            }

            if (_fishStatus.IsWashed)
            {
                Debug.LogWarning("Fish is already washed");
                return;
            }

            UpdateProgressUI(0);

            fish.sprite = _cookingManager.CurrentFish.icon;
            userInterface.SetActive(true);
        }

        public void Close() => userInterface.SetActive(false);

        public void UpdateProgressUI(int value) => progress.text = value + "%";
    }
}