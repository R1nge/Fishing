using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Restaurant
{
    public class BambooRollingMat : MonoBehaviour
    {
        [SerializeField] private Transform dishPosition;
        [SerializeField] private Transform[] positions;
        private List<IngredientSo> _ingredients;
        private Recipes _recipes;
        private GameObject _currentFish;

        private void Awake()
        {
            _recipes = FindObjectOfType<Recipes>();
            _ingredients = new List<IngredientSo>(5);
        }

        public void Add(IngredientSo ingredient)
        {
            if (_ingredients.Count < _ingredients.Capacity)
            {
                if (_ingredients.Count != 0 && ingredient == _ingredients[^1])
                {
                    ingredient.data.tempAmount++;
                    var amountText = _currentFish.GetComponentInChildren<TextMeshProUGUI>();
                    amountText.text = ingredient.data.tempAmount.ToString();
                    ingredient.Decrease(1);
                }
                else
                {
                    _currentFish = Instantiate(ingredient.prefab, positions[_ingredients.Count]);
                    _currentFish.transform.localPosition = Vector3.zero;
                    _currentFish.transform.localScale = new Vector3(.32f , .32f, 1);
                    //instance.transform.localScale = new Vector3(.32f / _ingredients.Capacity, .32f, 1);
                    var amountText = _currentFish.GetComponentInChildren<TextMeshProUGUI>();
                    ingredient.data.tempAmount++;
                    amountText.text = ingredient.data.tempAmount.ToString();
                    ingredient.Decrease(1);
                    _ingredients.Add(ingredient);
                }
            }
        }

        private void OnMouseDown() => TryMake();

        private void TryMake()
        {
            for (int i = 0; i < _recipes.recipes.Count; i++)
            {
                var amountOfEqualElements = 0;
                if (_recipes.recipes[i].ingredients.Count == _ingredients.Count)
                {
                    for (int j = 0; j < _recipes.recipes[i].ingredients.Count; j++)
                    {
                        if (_recipes.recipes[i].amounts[j] == _ingredients[j].data.tempAmount)
                        {
                            if (_recipes.recipes[i].ingredients[j] == _ingredients[j])
                            {
                                amountOfEqualElements++;
                                if (amountOfEqualElements == _ingredients.Count)
                                {
                                    var instance = Instantiate(_recipes.recipes[i].prefab, dishPosition);
                                    instance.GetComponent<Dish>().SetDish(_recipes.recipes[i].dish);
                                    print("Spawned");
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            ResetTempAmount();
            DestroyChildren();
            _ingredients.Clear();
        }

        private void ResetTempAmount()
        {
            for (int i = 0; i < _ingredients.Count; i++)
            {
                _ingredients[i].data.tempAmount = 0;
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