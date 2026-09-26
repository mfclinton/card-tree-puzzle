using System.Collections.Generic;
using System.Linq;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Cards.Enums;
using CardGame.Core.State;
using CardGame.Core.Tree.Base;

namespace CardGame.Core.Cards.Base
{
    public class Card
    {
        public string Name { get; }
        public TargetType TargetType { get; }
        public List<ICardEffect> Effects { get; }

        public Card(string name, TargetType targetType, List<ICardEffect> effects)
        {
            Name = name;
            TargetType = targetType;
            Effects = effects;
        }

        public bool ValidateTarget(TreeNode target, GameState state)
        {
            bool isChild = target.Parent == state.CurrentNode;
            bool isSelf = target == state.CurrentNode;
            
            if (TargetType == TargetType.Location)
                return isSelf;
            if (TargetType == TargetType.Subtree)
                return isChild;
            
            return true;
        }

        public Card Clone()
        {
            return new Card(Name, TargetType, Effects.ToList());
        }
    }
}