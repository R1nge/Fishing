using FishingRod;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI length, weight, speed;
        [SerializeField] private FishingRodSo so;

        private void Awake() => so.OnStatsChanged += UpdateUI;

        private void Start() => UpdateUI();

        private void UpdateUI()
        {
            icon.sprite = so.icon;
            speed.text = "Speed: " + so.data.horizontalSpeed;
            weight.text = "Weight: " + so.data.maxWeight;
            length.text = "Length: " + so.data.maxLength;
        }

        private void OnDestroy() => so.OnStatsChanged -= UpdateUI;
    }
}