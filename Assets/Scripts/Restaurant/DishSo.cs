using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "Dish", menuName = "Restaurant/Dish")]
    public class DishSo : ScriptableObject
    {
        public string title;
        public int price;
    }
}