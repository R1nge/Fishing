// using Cysharp.Text;
// using TMPro;
// using UnityEngine;
//
// namespace Restaurant.UI
// {
//     public class TimerUI : MonoBehaviour
//     {
//         
// #if UNITY_EDITOR
//         private bool _isQuiting;
// #endif
//         private void Start() => Timer.Instance.OnTimeUpdated += UpdateUI;
//
//         
//
// #if UNITY_EDITOR
//         private void OnApplicationQuit() => _isQuiting = true;
// #endif
//
//         private void OnDestroy()
//         {
// #if UNITY_EDITOR
//             if (_isQuiting) return;
// #endif
//             Timer.Instance.OnTimeUpdated -= UpdateUI;
//         }
//     }
// }