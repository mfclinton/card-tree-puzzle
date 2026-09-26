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
    public class DetectWithinDepthEffect : IInformationEffect
    {
        private readonly int _scanDepth;
        private readonly NodeType _targetType;
        
        public InformationType OutputType => InformationType.Boolean;

        public DetectWithinDepthEffect(int scanDepth, NodeType targetType)
        {
            _scanDepth = scanDepth;
            _targetType = targetType;
        }
        
        public InformationResult GetInformation(TreeNode target, GameState state)
        {
            bool isWithinDepth = TreeNodeHelpers.GetNodesWithinDepth(target, _scanDepth).Any(node => node.Type == _targetType);
            return new InformationResult(InformationType.Boolean, isWithinDepth);
        }

        public bool ValidateTarget(TreeNode target, GameState state)
        {
            return true;
        }
        
        public string GetDescription()
        {
            return $"Detect if there is a {_targetType} within the next {_scanDepth} levels of the tree.";
        }
    }
}