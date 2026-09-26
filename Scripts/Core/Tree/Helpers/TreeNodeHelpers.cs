using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Enums;

namespace CardGame.Core.Tree.Helpers
{
    public static class TreeNodeHelpers
    {
        public static IEnumerable<TreeNode> GetNodesWithinDepth(TreeNode startNode, int depth, bool useStartNode = true)
        {
            // Base Case
            if (depth < 0)
                yield break;

            // Yield Node
            if (useStartNode)
                yield return startNode;

            // Recursive Case
            foreach (var child in startNode.Children)
            {
                foreach (var node in GetNodesWithinDepth(child, depth - 1))
                {
                    yield return node;
                }
            }
        }

        public static IEnumerable<Tuple<TreeNode, int>> GetNodesWithinDepthWithDistance(TreeNode startNode, int maxDepth, int curDepth = 0, bool useStartNode = true)
        {
            // Base Case
            if (curDepth > maxDepth)
                yield break;

            // Yield Node
            if (useStartNode)
                yield return Tuple.Create(startNode, curDepth);
            
            // Recursive Case
            foreach (var child in startNode.Children)
            {
                foreach (var node in GetNodesWithinDepthWithDistance(child, maxDepth, curDepth + 1))
                {
                    yield return node;
                }
            }
        }
        
        public static int FindShortestPathToType(TreeNode startNode, NodeType type)
        {
            // Found Type
            if (startNode.Type == type)
                return 0;
            
            // No Children
            if (!startNode.Children.Any())
                return int.MaxValue;

            // Check Children
            var shortestChildPath = startNode.Children
                .Select(child => FindShortestPathToType(child, type))
                .Min();

            return shortestChildPath == int.MaxValue ? int.MaxValue : shortestChildPath + 1;
        }
        
        public static bool ContainsPattern(TreeNode startNode, NodeType[] pattern)
        {
            // No Pattern
            if (pattern == null || pattern.Length == 0)
                return true;
            
            // Pattern Mismatch
            if (startNode.Type != pattern[0])
                return false;
            
            // Pattern Found
            if (pattern.Length == 1)
                return true;

            // Recursive Case
            var remainingPattern = pattern.Skip(1).ToArray();
            return startNode.Children.Any(child => ContainsPattern(child, remainingPattern));
        }
    }
}