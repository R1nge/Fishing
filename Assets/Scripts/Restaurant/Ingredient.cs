using UnityEngine;

namespace Restaurant
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientSo ingredientSo;
        [SerializeField] private SpriteRenderer icon;
        private BambooRollingMat _bamboo;

        private void Awake()
        {
            _bamboo = FindObjectOfType<BambooRollingMat>();
            icon.sprite = ingredientSo.icon;
        }

        private void OnMouseDown()
        {
            if (ingredientSo.data.amount > 0)
            {
                _bamboo.Add(ingredientSo);
            }
            else
            {
                Debug.LogWarning("Amount <= 0");
            }
        }
    }
}