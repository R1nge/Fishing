using Fish;
using Restaurant;
using UnityEngine;

public class HookCollision : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public void Attach(Collider2D obj)
    {
        if (obj.TryGetComponent(out FishMovementController movementController))
        {
            movementController.enabled = false;
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
        }
    }

    public void AddToInventory(IngredientSo fish) => inventory.Add(fish);
}