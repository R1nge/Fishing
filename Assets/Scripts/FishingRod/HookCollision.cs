using Fish;
using Restaurant;
using UnityEngine;

namespace FishingRod
{
    public class HookCollision : MonoBehaviour
    {
        public void Attach(Collider2D obj)
        {
            if (obj.TryGetComponent(out FishMovementController movementController))
            {
                movementController.enabled = false;
                obj.transform.parent = transform;
                obj.transform.localPosition = Vector3.zero;
            }
        }

        public void AddToInventory(IngredientSo fish) => Inventory.Instance.Add(fish);
    }
}