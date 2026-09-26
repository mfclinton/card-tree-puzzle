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
    public class CountWithinDepthEffect : IInformationEffect
    {
        private readonly int _scanDepth;
        private readonly NodeType _targetType;
        
        public InformationType OutputType => InformationType.Number;

        public CountWithinDepthEffect(int scanDepth, NodeType targetType)
        {
            _scanDepth = scanDepth;
            _targetType = targetType;
        }
        
        public InformationResult GetInformation(TreeNode target, GameState state)
        {
            int count = TreeNodeHelpers.GetNodesWithinDepth(target, _scanDepth).Count(node => node.Type == _targetType);
            return new InformationResult(InformationType.Number, count);
        }

        public bool ValidateTarget(TreeNode target, GameState state)
        {
            return true;
        }
        
        public string GetDescription()
        {
            return $"Count the number of {_targetType} within the next {_scanDepth} levels of the tree.";
        }
    }
}