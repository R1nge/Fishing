using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class BambooRollingMat : MonoBehaviour
    {
        [SerializeField] private Transform dishPosition;
        [SerializeField] private Transform[] positions;
        private List<IngredientSo> _ingredients;
        private Recipes _recipes;

        private void Awake()
        {
            _recipes = FindObjectOfType<Recipes>();
            _ingredients = new List<IngredientSo>(5);
        }

        public void Add(IngredientSo ingredient)
        {
            if (_ingredients.Count < _ingredients.Capacity)
            {
                var instance = Instantiate(ingredient.prefab, positions[_ingredients.Count]);
                instance.transform.localPosition = Vector3.zero;
                instance.transform.localScale = new Vector3(.32f / _ingredients.Capacity, .32f, 1);
                _ingredients.Add(ingredient);
            }
        }

        private void OnMouseDown() => TryMake();

        private void TryMake()
        {
            for (int i = 0; i < _recipes.recipes.Count; i++)
            {
                var amountOfEqualElements = 0;
                for (int j = 0; j < _recipes.recipes[i].ingredients.Count; j++)
                {
                    if (_recipes.recipes[i].ingredients.Count == _ingredients.Count)
                    {
                        if (_recipes.recipes[i].ingredients[j] == _ingredients[j])
                        {
                            amountOfEqualElements++;
                            if (amountOfEqualElements == _ingredients.Count)
                            {
                                var instance = Instantiate(_recipes.recipes[i].prefab, dishPosition);
                                instance.GetComponent<Dish>().SetDish(_recipes.recipes[i].dish);
                                break;
                            }
                        }
                    }
                }
            }

            TakeIngredients();
            DestroyChildren();
            _ingredients.Clear();
        }

        private void TakeIngredients()
        {
            for (int i = 0; i < _ingredients.Count; i++)
            {
                _ingredients[i].Decrease(1);
            }
        }

        private void DestroyChildren()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (positions[i].childCount == 0) continue;
                Destroy(positions[i].GetChild(0).gameObject);
            }
        }
    }
}