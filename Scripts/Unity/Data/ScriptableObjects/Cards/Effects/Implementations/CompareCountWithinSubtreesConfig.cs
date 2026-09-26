using System;
using CardGame.Core.Cards.Effects.Implementations;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Tree.Enums;
using CardGame.Unity.ScriptableObjects.Cards.Effects.Enums;

namespace CardGame.Unity.ScriptableObjects.Cards.Effects.Implementations
{
    [Serializable]
    public class CompareCountWithinSubtreesConfig : EffectConfig
    {
        public int scanDepth;
        public NodeType targetType;
        public ComparisonType comparisonType;

        public override ICardEffect CreateEffect()
        {
            return new CompareCountWithinSubtreesEffect(scanDepth, targetType, comparisonType.GetFunction());
        }
    }
}