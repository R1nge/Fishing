using FishingRod;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI length, weight, speed, price, buttonText;
        [SerializeField] private Button button;
        [SerializeField] private FishingRodSo current, so;

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
            speed.text = "Speed: " + so.data.horizontalSpeed;
            weight.text = "Weight: " + so.data.maxWeight;
            length.text = "Length: " + so.data.maxLength;
            price.text = "Price: " + so.price;
        }

        private void UpdateStatus()
        {
            button.interactable = current.data.rodSprite != so.data.rodSprite;
            buttonText.text = button.interactable ? so.data.isUnlocked ? "Equip" : "Buy" : so.data.isUnlocked ? "Equipped" : "Equip";
            price.gameObject.SetActive(!so.data.isUnlocked);
        }

        private void OnDestroy()
        {
            so.OnStatsChanged -= UpdateUI;
            current.OnStatusChanged -= UpdateStatus;
        }
    }
}