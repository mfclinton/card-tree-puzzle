using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Core.Cards.Base;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Cards.Enums;
using CardGame.Core.State;
using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Enums;
using CardGame.Core.Tree.Helpers;

namespace CardGame.Core.Cards.Effects.Implementations
{
    public delegate bool SubtreeComparison(int count, int otherCount);
    
    public class CompareCountWithinSubtreesEffect : IInformationEffect
    {
        private readonly int _scanDepth;
        private readonly NodeType _targetType;
        private readonly SubtreeComparison _comparator;
        
        public InformationType OutputType => InformationType.Subtree;

        public CompareCountWithinSubtreesEffect(int scanDepth, NodeType targetType, SubtreeComparison comparator)
        {
            _scanDepth = scanDepth;
            _targetType = targetType;
            _comparator = comparator;
        }
        
        public InformationResult GetInformation(TreeNode target, GameState state)
        {
            int count = TreeNodeHelpers.GetNodesWithinDepth(target, _scanDepth).Count(node => node.Type == _targetType);
            
            // Compare Other Children
            List<TreeNode> matchingResults = new();
            foreach (var node in state.CurrentNode.Children)
            {
                if (node == target)
                    continue;
                
                int otherCount = TreeNodeHelpers.GetNodesWithinDepth(node, _scanDepth).Count(node => node.Type == _targetType);
                
                if (_comparator(count, otherCount))
                    matchingResults.Add(node);
            }
            
            // Random Result
            var random = new Random();
            var chosenNode = matchingResults.OrderBy(_ => random.Next()).FirstOrDefault();
            
            return new InformationResult(InformationType.Subtree, chosenNode);
        }

        public bool ValidateTarget(TreeNode target, GameState state)
        {
            bool isChild = target.Parent == state.CurrentNode;
            return isChild;
        }

        public string GetDescription()
        {
            return $"Compare the count of {_targetType} within the next {_scanDepth} levels of the tree.";
        }
    }
}