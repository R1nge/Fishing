using TMPro;
using UnityEngine;

namespace Restaurant.UI
{
    public class OutsideTableUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Tables tables;
        private void Awake() => tables.data.OnValueChanged += UpdateUI;

        private void Start() => tables.data.Update();

        private void UpdateUI(int amount, int maxAmount) => text.SetText(amount + "/" + maxAmount);

        private void OnDestroy() => tables.data.OnValueChanged -= UpdateUI;
    }
}