using FishingRod;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SaveManagerInstaller : MonoInstaller
    {
        [SerializeField] private SaveManager saveManager;

        public override void InstallBindings()
        {
            var saveManagerInstance = Container.InstantiatePrefabForComponent<SaveManager>(saveManager);
            Container.Bind<SaveManager>().FromInstance(saveManagerInstance).AsSingle();
            Container.QueueForInject(saveManager);
        }
    }
}