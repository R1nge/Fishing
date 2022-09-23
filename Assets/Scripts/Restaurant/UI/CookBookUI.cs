using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.UI
{
    public class CookBookUI : MonoBehaviour
    {
        [SerializeField] private GameObject cookBook;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;
        [SerializeField] private float verticalDistance;
        [SerializeField] private ScrollRect scrollRect;
        private Recipes _recipes;

        private void Awake()
        {
            _recipes = FindObjectOfType<Recipes>();
            Close();
        }

        public void Open()
        {
            if (cookBook.activeSelf) return;
            cookBook.SetActive(true);

            for (var i = 0; i < _recipes.recipes.Count; i++)
            {
                var instance = Instantiate(prefab, parent);
                instance.transform.position += new Vector3(0, -i * verticalDistance);
                var result = instance.GetComponentInChildren<Image>();
                result.sprite = _recipes.recipes[i].prefab.GetComponent<SpriteRenderer>().sprite;
                instance.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = _recipes.recipes[i].name;

                for (int j = 0; j < _recipes.recipes[i].ingredients.Count; j++)
                {
                    var ingredient = _recipes.recipes[i].ingredients[j];
                    GameObject go = new GameObject(ingredient.title);
                    go.transform.parent = instance.transform.GetChild(1);
                    go.transform.localPosition = Vector3.zero;
                    var icon = go.AddComponent<Image>();
                    icon.sprite = ingredient.icon;
                    GameObject amount = new GameObject("Amount");
                    amount.transform.parent = go.transform;
                    amount.transform.localPosition = new Vector3(40,-30,0);
                    var amountText = amount.AddComponent<TextMeshProUGUI>();
                    amountText.text = _recipes.recipes[i].amounts[j].ToString();
                    amountText.alignment = TextAlignmentOptions.Center;
                    amountText.horizontalAlignment = HorizontalAlignmentOptions.Center;
                    amountText.color = Color.white;
                    amountText.fontStyle = FontStyles.Bold;
                }
            }

            ScrollToTop();
        }

        public void Close()
        {
            cookBook.SetActive(false);

            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        private void ScrollToTop() => scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}