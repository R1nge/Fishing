using UnityEngine;

namespace Restaurant
{
    public class Dishes : MonoBehaviour
    {
        private Inventory _inventory;
        private CookingManager _cookingManager;
        private CookingRecipesManager _cookingRecipesManager;
        private FishStatus _fishStatus;

        private void Awake()
        {
            _inventory = FindObjectOfType<Inventory>();
            _cookingManager = FindObjectOfType<CookingManager>();
            _cookingRecipesManager = FindObjectOfType<CookingRecipesManager>();
            _fishStatus = FindObjectOfType<FishStatus>();
        }

        private void OnMouseDown()
        {
            var hasFish = _cookingManager.CurrentFish == _cookingRecipesManager.Current.fish;
            var washed = _fishStatus.IsWashed == _cookingRecipesManager.Current.isWashed;
            var chopped = _fishStatus.IsChopped == _cookingRecipesManager.Current.isChopped;
            var cooked = _fishStatus.IsCooked == _cookingRecipesManager.Current.isCooked;

            if (hasFish && washed && chopped && cooked)
            {
                Instantiate(_cookingRecipesManager.Current.prefab, transform.position, Quaternion.identity);
                
                foreach (var fish in _inventory.fish)
                {
                    if (fish == _cookingManager.CurrentFish)
                    {
                        fish.data.amount--;
                        break;
                    }
                }

                _cookingManager.Delete();
                _fishStatus.ResetStatus();
            }
        }
    }
}