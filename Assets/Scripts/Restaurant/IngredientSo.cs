using System;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "Cooking Ingredient", menuName = "Restaurant/Cooking Ingredient")]
    public class IngredientSo : ScriptableObject
    {
        public string title;
        public Sprite icon;
        public GameObject prefab;
        public Data data;
    }

    [Serializable]
    public class Data
    {
        public int amount;
    }
}