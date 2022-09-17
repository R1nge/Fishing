using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class Recipes : MonoBehaviour
    {
        public List<CookingRecipeSo> recipes;

        public CookingRecipeSo Pick() => recipes[Random.Range(0, recipes.Count)];
    }
}