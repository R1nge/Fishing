using Other;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneControllerInstaller : MonoInstaller
    {
        [SerializeField] private SceneController sceneController;

        public override void InstallBindings()
        {
            var sceneControllerInstance = Container.InstantiatePrefabForComponent<SceneController>(sceneController);
            Container.Bind<SceneController>().FromInstance(sceneControllerInstance).AsSingle();
            Container.QueueForInject(sceneController);
        }
    }
}