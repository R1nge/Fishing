using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private MoneyManager moneyManager;

    public void Sell()
    {
        moneyManager.Earn(inventory.fish[0].TotalPrice);
    }
}