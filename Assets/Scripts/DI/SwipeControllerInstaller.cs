using Other;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SwipeControllerInstaller : MonoInstaller
    {
        [SerializeField] private SwipeController swipeController;

        public override void InstallBindings()
        {
            var swipeControllerInstance = Container.InstantiatePrefabForComponent<SwipeController>(swipeController);
            Container.Bind<SwipeController>().FromInstance(swipeControllerInstance).AsSingle();
            Container.QueueForInject(swipeControllerInstance);
        }
    }
}