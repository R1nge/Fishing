using Other;
using UnityEngine;
using Zenject;

namespace DI
{
    public class TimerInstaller : MonoInstaller
    {
        [SerializeField] private Timer timer;

        public override void InstallBindings()
        {
            var timerInstance = Container.InstantiatePrefabForComponent<Timer>(timer);
            Container.Bind<Timer>().FromInstance(timerInstance).AsSingle();
            Container.QueueForInject(timer);
        }
    }
}