using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class Table : MonoBehaviour
    {
        public CookingRecipeSo Current { get; private set; }
        private CookingRecipesManager _cookingRecipesManager;
        private MoneyManager _moneyManager;
        private TableUI _userInterface;

        private void Awake()
        {
            _cookingRecipesManager = FindObjectOfType<CookingRecipesManager>();
            _moneyManager = FindObjectOfType<MoneyManager>();
            _userInterface = GetComponent<TableUI>();
            Pick();
        }

        private void Pick()
        {
            _cookingRecipesManager.Pick();
            Current = _cookingRecipesManager.Current;
            _userInterface.SetText(Current.name);
        }

        private void OnMouseDown() => Select();

        private void Select()
        {
            if (_cookingRecipesManager.Current == Current)
                return;
            _cookingRecipesManager.Select(Current);
        }

        public void CompleteOrder()
        {
            _moneyManager.Earn(_cookingRecipesManager.Current.price);
            print("Order Completed " + "Price: " + _cookingRecipesManager.Current.price);
        }
    }
}