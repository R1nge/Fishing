using TMPro;
using UnityEngine;

namespace Restaurant.UI
{
    public class IngredientUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetAmount(int value) => text.text = value.ToString();
    }
}