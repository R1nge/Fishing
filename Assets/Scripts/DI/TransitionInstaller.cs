using Other;
using UnityEngine;
using Zenject;

namespace DI
{
    public class TransitionInstaller : MonoInstaller
    {
        [SerializeField] private Transition transition;

        public override void InstallBindings()
        {
            var transitionInstance = Container.InstantiatePrefabForComponent<Transition>(transition);
            Container.Bind<Transition>().FromInstance(transitionInstance).AsSingle();
            Container.QueueForInject(transition);
        }
    }
}