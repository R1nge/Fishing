using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class CookBook : MonoBehaviour
    {
        private CookBookUI _ui;

        private void Awake() => _ui = FindObjectOfType<CookBookUI>();

        private void OnMouseDown() => _ui.Open();
    }
}