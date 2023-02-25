using Fish;
using Restaurant;
using UnityEngine;
using Zenject;

namespace FishingRod
{
    public class HookCollision : MonoBehaviour
    {
        private Inventory _inventory;

        [Inject]
        public void Constructor(Inventory inventory) => _inventory = inventory;

        public void Attach(Collider2D obj)
        {
            if (obj.TryGetComponent(out FishMovementController movementController))
            {
                movementController.enabled = false;
                obj.transform.parent = transform;
                obj.transform.localPosition = Vector3.zero;
            }
        }

        public void AddToInventory(IngredientSo fish) => _inventory.Add(fish);
    }
}