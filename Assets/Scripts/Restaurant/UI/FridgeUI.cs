using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.UI
{
    public class FridgeUI : MonoBehaviour
    {
        [SerializeField] private GameObject userInterface;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;
        private Inventory _inventory;
        private CookingManager _cookingManager;

        private void Awake()
        {
            _cookingManager = FindObjectOfType<CookingManager>();
            _inventory = FindObjectOfType<Inventory>();
            Close();
        }

        public void Open()
        {
            userInterface.SetActive(true);

            for (int i = 0; i < _inventory.fish.Count; i++)
            {
                if (_inventory.fish[i].data.amount <= 0) continue;
                var slot = Instantiate(prefab, parent);
                slot.transform.position += new Vector3(100 * i, 0, 0);
                var image = slot.GetComponentInChildren<Image>();
                image.sprite = _inventory.fish[i].icon;
                var amountText = slot.GetComponentInChildren<TextMeshProUGUI>();
                amountText.text = _inventory.fish[i].data.amount.ToString();
                var button = slot.GetComponent<Button>();
                //Won't show up in the inspector
                var i1 = i;
                button.onClick.AddListener(() => _cookingManager.SelectFish(_inventory.fish[i1]));
                button.onClick.AddListener(Close);
            }
        }

        public void Close()
        {
            userInterface.SetActive(false);

            for (int i = parent.childCount - 1; i >= 2; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}