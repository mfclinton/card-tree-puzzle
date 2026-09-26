using System;
using CardGame.Core.Cards.Effects.Interfaces;

namespace CardGame.Unity.ScriptableObjects.Cards.Effects
{
    [Serializable]
    public abstract class EffectConfig
    {
        public abstract ICardEffect CreateEffect();
    }
}