using System;
using CardGame.Core.Cards.Effects.Implementations;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Tree.Enums;

namespace CardGame.Unity.ScriptableObjects.Cards.Effects.Implementations
{
    [Serializable]
    public class CountWithinDepthConfig : EffectConfig
    {
        public int scanDepth;
        public NodeType targetType;

        public override ICardEffect CreateEffect()
        {
            return new CountWithinDepthEffect(scanDepth, targetType);
        }
    }
}