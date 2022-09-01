using UnityEngine;

namespace Restaurant
{
    [System.Serializable]
    public class Order
    {
        [SerializeField] private Sprite customer;
        [SerializeField] private Sprite dish;
        [SerializeField] private bool finished;
        [SerializeField] private int tolerance;
        [SerializeField] private CookingRecipeSo order;

        public Sprite GetCustomer() => customer;
        public Sprite GetDish() => dish;
        public bool GetStatus() => finished;
        public int GetTolerance() => tolerance;
        public CookingRecipeSo GetOrder() => order;

        public void SetSprite(Sprite sprite) => customer = sprite;
        public void SetDish(Sprite sprite) => dish = sprite;
        public void SetStatus(bool value) => finished = value;
        public void SetTolerance(int value) => tolerance = value;
        public void SetOrder(CookingRecipeSo recipe) => order = recipe;
    }
}