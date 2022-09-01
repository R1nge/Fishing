using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "Cooking Recipe", menuName = "Restaurant/Cooking Recipe")]
    public class CookingRecipeSo : ScriptableObject
    {
        public List<IngredientSo> ingredients;
        public GameObject prefab;
        public DishSo dish;
    }
}