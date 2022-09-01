using TMPro;
using UnityEngine;

namespace Restaurant.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
#if UNITY_EDITOR
        private bool _isQuiting;
#endif
        private void Start() => Timer.Instance.OnTimeUpdated += UpdateUI;

        private void UpdateUI(int hours, int minutes) => text.SetText($"{hours:00}:{minutes:00}");

#if UNITY_EDITOR
        private void OnApplicationQuit() => _isQuiting = true;
#endif

        private void OnDestroy()
        {
#if UNITY_EDITOR
            if (_isQuiting) return;
#endif
            Timer.Instance.OnTimeUpdated -= UpdateUI;
        }
    }
}