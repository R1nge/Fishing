using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.UI
{
    public class CookBookUI : MonoBehaviour
    {
        [SerializeField] private GameObject cookBook;
        [SerializeField] private List<CookingRecipeSo> recipes;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;
        private void Awake() => Close();

        public void Open()
        {
            cookBook.SetActive(true);

            for (var i = 0; i < recipes.Count; i++)
            {
                var instance = Instantiate(prefab, parent);
                instance.transform.position += new Vector3(0, -i * 100);
                var image = instance.GetComponentInChildren<Image>();
                image.sprite = recipes[i].fish.icon;
            }
        }

        public void Close()
        {
            cookBook.SetActive(false);
            
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}