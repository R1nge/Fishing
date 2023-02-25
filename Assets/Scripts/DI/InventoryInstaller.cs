using Restaurant;
using UnityEngine;
using Zenject;

namespace DI
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private Inventory inventory;

        public override void InstallBindings()
        {
            var inventoryInstance = Container.InstantiatePrefabForComponent<Inventory>(inventory);
            Container.Bind<Inventory>().FromInstance(inventoryInstance).AsSingle();
            Container.QueueForInject(inventory);
        }
    }
}