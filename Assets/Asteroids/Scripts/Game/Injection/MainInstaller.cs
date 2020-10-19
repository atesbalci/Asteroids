using Asteroids.Game.Behaviours.Asteroids;
using Asteroids.Game.Behaviours.Asteroids.Managers.Impl;
using Asteroids.Game.Behaviours.Players;
using Asteroids.Game.Behaviours.Players.Impl;
using Asteroids.Game.Behaviours.Projectiles;
using Asteroids.Helpers.Timing;
using Asteroids.Helpers.Timing.Impl;
using Asteroids.Scripts.Game.Controllers;
using Asteroids.Scripts.Game.Models.BoundedObjects;
using Asteroids.Scripts.Game.Models.BoundedObjects.Impl;
using Asteroids.Scripts.Game.Models.GameState;
using Asteroids.Scripts.Game.Models.Input.Impl;
using Asteroids.Scripts.Game.Models.Players;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Injection
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private PlayerBehaviour _playerBehaviour;
        
        [SerializeField] private Transform _projectilesParent;
        [SerializeField] private Transform _asteroidsParent;
        
        public override void InstallBindings()
        {
            BindModels();
            BindUtilities();
            BindBehaviours();
            BindControllers();
            BindPools();
        }

        private void BindModels()
        {
            Container.Bind<IBoundedObjectsList>().To<BoundedObjectsData>().AsSingle();
            Container.Bind<Player>().AsSingle();
            Container.Bind<GameStateData>().AsSingle();
        }

        private void BindUtilities()
        {
            Container.BindInterfacesAndSelfTo<SimpleGameInput>().AsSingle(); // Binds IGameInput
            Container.Bind<ITimingManager>().To<TweenTimingManager>().AsSingle();
        }

        private void BindBehaviours()
        {
            Container.BindInstance<IPlayerBehaviour>(_playerBehaviour).AsSingle();
            Container.BindInterfacesAndSelfTo<PoolAsteroidsManager>().AsSingle(); // Binds IAsteroidsManager
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<BoundariesController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        }

        private void BindPools()
        {
            Container.BindMemoryPool<ProjectileBehaviour, ProjectileBehaviour.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_projectilePrefab)
                .UnderTransform(_projectilesParent);
            Container.BindMemoryPool<AsteroidBehaviour, AsteroidBehaviour.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_asteroidPrefab)
                .UnderTransform(_asteroidsParent);
        }
    }
}
