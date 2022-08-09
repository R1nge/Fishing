using UnityEngine;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject play, pull, restaurant, shop;

        private void Start() => ShowOutGameMenu();

        public void ShowGamePlayMenu()
        {
            play.SetActive(false);
            pull.SetActive(true);
            restaurant.SetActive(false);
            shop.SetActive(false);
        }

        public void ShowOutGameMenu()
        {
            play.SetActive(true);
            pull.SetActive(false);
            restaurant.SetActive(true);
            shop.SetActive(true);
        }
    }
}