using FishingRod;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title, length, weight, speed, price, buttonText;
        [SerializeField] private Button button;
        [SerializeField] private FishingRodSo current, so;
        [SerializeField] private MoneyManager moneyManager;

        private void Awake()
        {
            so.OnStatsChanged += UpdateUI;
            current.OnStatusChanged += UpdateStatus;
        }

        private void Start()
        {
            UpdateUI();
            UpdateStatus();
        }

        private void UpdateUI()
        {
            icon.sprite = so.icon;
            title.SetText(so.data.rodTitle);
            speed.SetText("Speed: " + so.data.horizontalSpeed);
            weight.SetText("Weight: " + so.data.maxWeight);
            length.SetText("Length: " + so.data.maxLength);
            price.SetText("Price: " + so.price);
        }

        private void UpdateStatus()
        {
            if (current.data.rodSprite == so.data.rodSprite || moneyManager.Money < so.price && !so.data.isUnlocked)
                button.interactable = false;
            else
                button.interactable = true;

            if (button.interactable)
                buttonText.SetText(so.data.isUnlocked ? "Equip" : "Buy");
            else
                buttonText.SetText(so.data.isUnlocked ? "Equipped" : "Buy");

            price.gameObject.SetActive(!so.data.isUnlocked);
        }

        private void OnDestroy()
        {
            so.OnStatsChanged -= UpdateUI;
            current.OnStatusChanged -= UpdateStatus;
        }
    }
}