using UnityEngine;

namespace Restaurant
{
    public class OutsideTable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite) => spriteRenderer.sprite = sprite;
    }
}