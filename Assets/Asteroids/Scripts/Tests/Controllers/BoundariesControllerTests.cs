using System.Collections.Generic;
using Asteroids.Helpers.Bounds;
using Asteroids.Scripts.Game.Controllers;
using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.BoundedObjects;
using Asteroids.Scripts.Game.Models.BoundedObjects.Impl;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Tests.Controllers
{
    [TestFixture]
    public class BoundariesControllerTests
    {
        private BoundariesController _boundariesController;
        private IBoundedObjectsList _boundedObjectsList;

        [SetUp]
        public void SetUp()
        {
            _boundedObjectsList = new BoundedObjectsData();
            var boundProvider = Substitute.For<IBoundProvider>();
            boundProvider.GetWorldPosition(Arg.Any<Vector2>()).Returns(info => info.Arg<Vector2>());
            _boundariesController = new BoundariesController(_boundedObjectsList, boundProvider);
        }
        
        [Test]
        public void TestObjectPositionWrapping()
        {
            var initialPositions = new[]
            {
                new Vector2(0.5f, 0.5f),
                new Vector2(1.1f + GameRules.BoundOffset / 2, 0.5f),
                new Vector2(0.5f, -1.1f - GameRules.BoundOffset / 2)
            };

            var objects = new IBoundedObject[initialPositions.Length];
            for (int i = 0; i < initialPositions.Length; i++)
            {
                objects[i] = new MockBoundObject {Position = initialPositions[i]};
            }
            
            foreach (var boundObject in objects)
            {
                _boundedObjectsList.Add(boundObject);
            }
            
            _boundariesController.FixedTick();
            Assert.AreEqual(initialPositions[0], objects[0].Position, "Object 0 shouldn't have moved.");
            Assert.IsTrue(objects[1].Position.x < 0f, "Object 1 should have wrapped.");
            Assert.IsTrue(objects[2].Position.y > 0f, "Object 2 should have wrapped.");
        }

        private class MockBoundObject : IBoundedObject
        {
            public Vector2 Position { get; set; }
        }
    }
}