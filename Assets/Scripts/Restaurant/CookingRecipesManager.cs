using UnityEngine;

namespace Restaurant
{
    public class CookingRecipesManager : MonoBehaviour
    {
        public CookingRecipeSo Current { get; private set; }
        private Recipes _recipes;

        private void Awake() => _recipes = FindObjectOfType<Recipes>();
        
        public void Pick()
        {
            var index = Random.Range(0, _recipes.recipes.Count);
            Current = _recipes.recipes[index];

            if (Current.fish.data.amount <= 0)
            {
                Pick();
            }
        }

        public void Select(CookingRecipeSo recipe) => Current = recipe;
    }
}