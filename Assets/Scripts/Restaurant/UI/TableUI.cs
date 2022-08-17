using TMPro;
using UnityEngine;

namespace Restaurant.UI
{
    public class TableUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetText(string value) => text.text = value;
    }
}