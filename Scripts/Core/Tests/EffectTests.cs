using CardGame.Core.Cards.Effects.Implementations;
using CardGame.Core.State;
using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Enums;
using CardGame.Core.Tests.TestHelpers;
using NUnit.Framework;


namespace CardGame.Core.Tests
{
    [TestFixture]
    public class EffectTests
    {
        private TreeNode _rootNode;
        private GameState _gameState;

        [SetUp]
        public void Setup()
        {
            // Create Tree
            _rootNode = TreeTestHelper.CreateStandardTestTree();

            // Initialize Game State
            _gameState = new GameState();
            _gameState.InitializeGameState(_rootNode, new(), 0);
        }


        [Test]
        public void CountWithinDepth_CountMonsters_CorrectCount()
        {
            // Arrange
            var effect = new CountWithinDepthEffect(2, NodeType.Monster);

            // Act
            var result = effect.GetInformation(_rootNode, _gameState);

            // Assert
            Assert.That(result.Value, Is.EqualTo(3), "Should find 3 monsters within depth 2 of root");
        }

        [Test]
        public void DetectWithinDepth_FindExit_FindsExitNode()
        {
            // Arrange
            var effect = new DetectWithinDepthEffect(2, NodeType.Exit);

            // Act
            var result = effect.GetInformation(_rootNode, _gameState);

            // Assert
            Assert.That(result.Value, Is.True, "Should detect exit node within depth 2");
        }

        [Test]
        public void DetectWithinDepth_TooShallow_DoesNotFindExit()
        {
            // Arrange
            var effect = new DetectWithinDepthEffect(1, NodeType.Exit);

            // Act
            var result = effect.GetInformation(_rootNode, _gameState);

            // Assert
            Assert.That(result.Value, Is.False, "Should not detect exit node with depth 1 from root");
        }

        [Test]
        public void CompareCountWithinSubtrees_CompareMonstersGreaterThan_CorrectResult()
        {
            // Arrange
            var effect = new CompareCountWithinSubtreesEffect(
                scanDepth: 2, 
                targetType: NodeType.Monster,
                comparator: (a, b) => a < b
            );

            // Act
            var result = effect.GetInformation(_rootNode.Children[0], _gameState);

            // Assert
            Assert.That(result.Value, Is.EqualTo(_rootNode.Children[1]),
                "Left subtree (1 monster) should be less than right subtree (2 monster)");
        }

        [Test]
        public void DetectWithinSubtree_FindMonster_DetectsInSubtree()
        {
            // Arrange
            var effect = new DetectWithinSubtreeEffect(1, NodeType.Monster);

            // Act
            var result = effect.GetInformation(_rootNode, _gameState);

            // Assert
            Assert.That(result.Value, Is.EqualTo(_rootNode.Children[0]),
                "Should detect monster in the 1st subtree");
        }
        
        [TestCase(1, 1)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        public void CountWithinDepth_DifferentDepths_CorrectCounts(int depth, int expectedCount)
        {
            // Arrange
            var effect = new CountWithinDepthEffect(depth, NodeType.Monster);

            // Act
            var result = effect.GetInformation(_rootNode, _gameState);

            // Assert
            Assert.That(result.Value, Is.EqualTo(expectedCount),
                $"Should find {expectedCount} monsters within depth {depth}");
        }
    }
}