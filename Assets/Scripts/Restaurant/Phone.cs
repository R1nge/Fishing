using UnityEngine;
using UnityEngine.Events;

namespace Restaurant
{
    public class Phone : MonoBehaviour
    {
        public UnityEvent unityEvent;

        private void OnMouseDown() => unityEvent?.Invoke();
    }
}