using TMPro;
using UnityEngine;

namespace UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private MoneyManager moneyManager;

        private void Awake() => moneyManager.OnMoneyAmountChanged += UpdateUI;

        private void UpdateUI(int value) => text.SetText(value.ToString());

        private void OnDestroy() => moneyManager.OnMoneyAmountChanged -= UpdateUI;
    }
}