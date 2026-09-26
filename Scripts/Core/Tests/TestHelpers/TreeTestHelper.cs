using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Enums;

namespace CardGame.Core.Tests.TestHelpers
{
    public static class TreeTestHelper
    {
        /// <summary>
        /// Creates a standard test tree with the following structure:
        ///           Normal
        ///       /            \
        ///  Monster           Normal
        ///     /              /    \
        ///  Exit         Monster   Monster
        /// </summary>
        public static TreeNode CreateStandardTestTree()
        {
            var rootNode = new TreeNode(NodeType.Normal);
            
            // Left Subtree
            var monsterNode1 = new TreeNode(NodeType.Monster);
            var exitNode1 = new TreeNode(NodeType.Exit);
            monsterNode1.AddChild(exitNode1);
            
            // Right Subtree
            var regularNode = new TreeNode(NodeType.Normal);
            var monsterNode2 = new TreeNode(NodeType.Monster);
            var monsterNode3 = new TreeNode(NodeType.Monster);
            
            regularNode.AddChild(monsterNode2);
            regularNode.AddChild(monsterNode3);
            
            // Add Subtrees
            rootNode.AddChild(monsterNode1);
            rootNode.AddChild(regularNode);

            return rootNode;
        }
    }
} 