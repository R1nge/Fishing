using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class Sleep : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void OnEnable() => button.onClick.AddListener(delegate { Timer.Instance.SetTime(6, 0); });

        private void OnDisable() => button.onClick.RemoveAllListeners();
    }
}