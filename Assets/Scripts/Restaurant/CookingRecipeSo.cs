using Fish;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "Cooking Recipe", menuName = "Restaurant/Cooking Recipe")]
    public class CookingRecipeSo : ScriptableObject
    {
        public FishSO fish;
        public bool isWashed;
        public bool isChopped;
        public bool isCooked;
        public GameObject prefab;
        public int price;
    }
}