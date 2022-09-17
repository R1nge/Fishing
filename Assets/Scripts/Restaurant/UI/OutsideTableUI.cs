using TMPro;
using UnityEngine;

namespace Restaurant.UI
{
    public class OutsideTableUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Awake() => TablesData.OnValueChanged += UpdateUI;

        private void Start() => TablesData.Update();

        private void UpdateUI(int amount, int maxAmount) => text.SetText(amount + "/" + maxAmount);

        private void OnDestroy() => TablesData.OnValueChanged -= UpdateUI;
    }
}