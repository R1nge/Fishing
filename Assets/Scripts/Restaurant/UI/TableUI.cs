using TMPro;
using UnityEngine;

namespace Restaurant.UI
{
    public class TableUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI order;
        [SerializeField] private TextMeshProUGUI tolerance;
        [SerializeField] private SpriteRenderer renderer;

        public void UpdateOrder(string value) => order.SetText(value);

        public void UpdateTolerance(string value) => tolerance.SetText(value);

        public void UpdateSprite(Sprite sprite) => renderer.sprite = sprite;

        public Sprite GetSprite() => renderer.sprite;
    }
}