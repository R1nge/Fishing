using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class CookingRecipesManager : MonoBehaviour
    {
        public CookingRecipeSo Current { get; private set; }
        [SerializeField] private List<CookingRecipeSo> recipes;

        public void Pick()
        {
            var index = Random.Range(0, recipes.Count);
            Current = recipes[index];

            if (Current.fish.data.amount <= 0)
            {
                Pick();
            }
        }

        public void Select(CookingRecipeSo recipe) => Current = recipe;
    }
}