using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private UnityEvent onGameStarted, onGameEnded;

        private void Start() => ShowOutGameMenu();

        public void ShowGamePlayMenu() => onGameStarted?.Invoke();

        public void ShowOutGameMenu() => onGameEnded?.Invoke();
    }
}