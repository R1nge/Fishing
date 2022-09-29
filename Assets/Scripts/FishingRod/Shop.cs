using Other;
using UnityEngine;

namespace FishingRod
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private FishingRodSo current;
        [SerializeField] private MoneyManager moneyManager;

        public void BuyRod(FishingRodSo so)
        {
            if (so.data.isUnlocked)
            {
                current.SetAll(so);
                return;
            }

            if (moneyManager.Spend(so.price))
            {
                so.Unlock(so);
                current.SetAll(so);
            }
        }
    }
}