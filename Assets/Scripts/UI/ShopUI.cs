using UnityEngine;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private GameObject openButton, closeButton, items, money;

        private void Start() => Close();

        public void Open()
        {
            openButton.SetActive(false);
            closeButton.SetActive(true);
            items.SetActive(true);
            money.SetActive(true);
        }

        public void Close()
        {
            openButton.SetActive(true);
            closeButton.SetActive(false);
            items.SetActive(false);
            money.SetActive(false);
        }
    }
}