using Restaurant;
using UnityEngine;
using Zenject;

namespace DI
{
    public class OrderInstaller : MonoInstaller
    {
        [SerializeField] private OrdersData order;

        public override void InstallBindings()
        {
            var orderInstance = Container.InstantiatePrefabForComponent<OrdersData>(order);
            Container.Bind<OrdersData>().FromInstance(orderInstance).AsSingle();
            Container.QueueForInject(orderInstance);
        }
    }
}