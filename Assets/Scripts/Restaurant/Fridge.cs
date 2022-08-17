using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class Fridge : MonoBehaviour
    {
        private FridgeUI _fridgeUI;

        private void Awake() => _fridgeUI = FindObjectOfType<FridgeUI>();

        private void OnMouseDown() => _fridgeUI.Open();
    }
}