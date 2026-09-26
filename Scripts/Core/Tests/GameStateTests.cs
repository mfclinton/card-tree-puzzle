using System.Collections.Generic;
using CardGame.Core.Cards.Base;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Cards.Enums;
using CardGame.Core.State;
using CardGame.Core.Tests.TestHelpers;
using CardGame.Core.Tree.Base;
using NUnit.Framework;

namespace CardGame.Core.Tests
{
    [TestFixture]
    public class GameStateTests
    {
        private int initialHandSize;
        
        private TreeNode _rootNode;
        private GameState _gameState;
        private List<Card> _initialDeck;

        [SetUp]
        public void Setup()
        {
            // Create Tree
            _rootNode = TreeTestHelper.CreateStandardTestTree();

            // Initialize deck
            _initialDeck = new List<Card>();
            for (int i = 0; i < 10; i++)
                _initialDeck.Add(new Card("Test Card", TargetType.All, new List<ICardEffect>()));

            initialHandSize = 5;
            _gameState = new GameState();
            _gameState.InitializeGameState(_rootNode, _initialDeck, initialHandSize);
        }

        [Test]
        public void GameState_InitialState_CorrectSetup()
        {
            // Assert
            Assert.That(_gameState.CurrentNode, Is.EqualTo(_rootNode), "Current node should be root node");
            Assert.That(_gameState.Hand.Count, Is.EqualTo(initialHandSize), $"Initial hand size should be {initialHandSize}");
            Assert.That(_gameState.Deck.Count, Is.EqualTo(_initialDeck.Count - initialHandSize), "Deck should match initial deck");
        }

        [Test]
        public void GameState_MoveToChild_CorrectNodeTransition()
        {
            // Arrange
            var targetNode = _rootNode.Children[0];

            // Act
            _gameState.MoveToNode(targetNode);

            // Assert
            Assert.That(_gameState.CurrentNode, Is.EqualTo(targetNode), 
                "Current node should update to target node");
        }

        [TestCase(0)]
        [TestCase(1)]
        public void GameState_ValidChildIndex_CanAccessChild(int childIndex)
        {
            // Act
            var child = _gameState.CurrentNode.Children[childIndex];

            // Assert
            Assert.That(child, Is.Not.Null, $"Child at index {childIndex} should exist");
            Assert.That(_rootNode.Children.Contains(child), Is.True, 
                $"Child at index {childIndex} should be in root's children");
        }
    }
}