﻿using UnityEngine;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private GameObject playButton, openButton, closeButton, items, money;

        private void Start() => Close();

        public void Open()
        {
            playButton.SetActive(false);
            openButton.SetActive(false);
            closeButton.SetActive(true);
            items.SetActive(true);
            money.SetActive(true);
        }

        public void Close()
        {
            playButton.SetActive(true);
            openButton.SetActive(true);
            closeButton.SetActive(false);
            items.SetActive(false);
            money.SetActive(false);
        }
    }
}