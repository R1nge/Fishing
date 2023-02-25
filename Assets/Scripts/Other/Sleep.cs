using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class Sleep : MonoBehaviour
    {
        [SerializeField] private Button button;
        private Timer _timer;

        private void Awake()
        {
            _timer = GameObject.FindWithTag("GameManager").GetComponentInChildren<Timer>();
            button.onClick.AddListener(delegate { _timer.SetTime(6, 0); });
        }

        private void OnDisable() => button.onClick.RemoveAllListeners();
    }
}