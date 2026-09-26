using System;
using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Generator;
using CardGame.Core.Tree.Generator.Setting;
using CardGame.Core.Tree.Generator.Settings;
using NUnit.Framework;

namespace CardGame.Core.Tests
{
    [TestFixture]
    public class TreeGeneratorTests
    {
        private TreeGenerator _treeGenerator;
        private GenerationConfig _generationConfig;
        
        [SetUp]
        public void Setup()
        {
            _treeGenerator = new TreeGenerator();
            _generationConfig = new GenerationConfig{
                MaxDepth = 3,
            };
        }

        [Test]
        public void GenerateTree_WithDefaultConfig_CreatesValidTree()
        {
            // Act
            var tree = _treeGenerator.GenerateTree(_generationConfig);

            // Assert
            Assert.That(tree, Is.Not.Null, "Generated tree should not be null");
            Assert.That(tree.Children, Is.Not.Empty, "Tree should have child nodes");
        }

        [Test]
        public void GenerateTree_WithSpecificDepth_RespectsMaxDepth()
        {
            // Arrange
            _generationConfig.MaxDepth = 3;

            // Act
            var tree = _treeGenerator.GenerateTree(_generationConfig);

            // Assert
            var maxDepth = CalculateMaxDepth(tree);
            Assert.That(maxDepth, Is.LessThanOrEqualTo(3), "Tree depth should not exceed configured max depth");
        }

        #region Helpers

        private int CalculateMaxDepth(TreeNode node)
        {
            // Base Case
            if (node.Children.Count == 0)
                return 0;

            // Recursive Case
            int maxChildDepth = 0;
            foreach (var child in node.Children)
            {
                maxChildDepth = Math.Max(maxChildDepth, CalculateMaxDepth(child));
            }
            
            // Self
            return maxChildDepth + 1;
        }

        private void AssertMaxBranching(TreeNode node, int maxBranching)
        {
            Assert.That(node.Children.Count, Is.LessThanOrEqualTo(maxBranching),
                "Node should not exceed max branching factor");

            foreach (var child in node.Children)
            {
                AssertMaxBranching(child, maxBranching);
            }
        }

        private int CountNodes(TreeNode node)
        {
            int count = 1;
            foreach (var child in node.Children)
            {
                count += CountNodes(child);
            }
            
            return count;
        }

        #endregion
    }
}