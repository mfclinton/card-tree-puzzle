using System;
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
    public class DetectWithinSubtreeEffect : IInformationEffect
    {
        private readonly int _scanDepth;
        private readonly NodeType _targetType;
        
        public InformationType OutputType => InformationType.Subtree;

        public DetectWithinSubtreeEffect(int scanDepth, NodeType targetType)
        {
            _scanDepth = scanDepth;
            _targetType = targetType;
        }
        
        public InformationResult GetInformation(TreeNode target, GameState state)
        {
            var random = new Random();
            var subtreeParent = TreeNodeHelpers.GetNodesWithinDepth(target, _scanDepth)
                .Where(node => node.Type == _targetType)
                .OrderBy(_ => random.Next())
                .FirstOrDefault();

            return new InformationResult(InformationType.Subtree, subtreeParent);
        }

        public bool ValidateTarget(TreeNode target, GameState state)
        {
            return target == state.CurrentNode;
        }
        
        public string GetDescription()
        {
            return $"Detect the parent of the first {_targetType} within the next {_scanDepth} levels of the tree.";
        }
    }
}