using Asteroids.Game.Behaviours.Asteroids;
using Asteroids.Game.Behaviours.Players;
using Asteroids.Game.Behaviours.Projectiles;
using Asteroids.Scripts.Game.Controllers;
using Asteroids.Scripts.Game.Models.BoundedObjects;
using Asteroids.Scripts.Game.Models.GameState;
using Asteroids.Scripts.Game.Models.Input;
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
            Container.Bind<IGameInput>().To<SimpleGameInput>().AsSingle();
            Container.Bind<IBoundedObjectsList>().To<BoundedObjectsData>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoundariesController>().AsSingle().NonLazy();
            Container.Bind<Player>().AsSingle();
            Container.Bind<GameStateData>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInstance(_playerBehaviour).AsSingle();
            Container.BindMemoryPool<ProjectileBehaviour, ProjectileBehaviour.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_projectilePrefab)
                .UnderTransform(_projectilesParent);
            Container.BindMemoryPool<AsteroidBehaviour, AsteroidBehaviour.Pool>()
                .WithInitialSize(30)
                .FromComponentInNewPrefab(_asteroidPrefab)
                .UnderTransform(_asteroidsParent);
        }
    }
}
