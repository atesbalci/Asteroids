using Asteroids.Scripts.Game.Controllers;
using Asteroids.Scripts.Game.Models.BoundedObjects;
using Asteroids.Scripts.Game.Models.Input;
using Asteroids.Scripts.Game.Models.Input.Impl;
using Zenject;

namespace Asteroids.Game.Injection
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameInput>().To<SimpleGameInput>().AsSingle();
            Container.Bind<BoundaryData>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoundariesController>().AsSingle().NonLazy();
        }
    }
}
