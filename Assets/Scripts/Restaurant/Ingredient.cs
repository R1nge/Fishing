using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientSo ingredientSo;
        [SerializeField] private SpriteRenderer icon;
        private BambooRollingMat _bamboo;
        private IngredientUI _ui;

        private void Awake()
        {
            _bamboo = FindObjectOfType<BambooRollingMat>();
            _ui = GetComponent<IngredientUI>();
            ingredientSo.OnAmountChanged += UpdateUI;
            icon.sprite = ingredientSo.icon;
        }

        private void Start() => UpdateUI(ingredientSo.data.amount);

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

        private void UpdateUI(int value) => _ui.SetAmount(value);

        private void OnDestroy()
        {
            if (ingredientSo == null) return;
            ingredientSo.OnAmountChanged -= UpdateUI;
        }
    }
}