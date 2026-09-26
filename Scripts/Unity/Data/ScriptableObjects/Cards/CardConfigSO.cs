using System.Collections.Generic;
using CardGame.Core.Cards.Base;
using CardGame.Core.Cards.Enums;
using UnityEngine;
using System.Linq;
using CardGame.Unity.ScriptableObjects.Cards.Effects;

namespace CardGame.Unity.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "Card", menuName = "CardGame/Card")]
    public class CardConfigSO : ScriptableObject
    {
        public string cardName;
        public TargetType targetType;
        [SerializeReference] public List<EffectConfig> effects;

        public Card CreateCard()
        {
            var cardEffects = effects.Select(e => e.CreateEffect()).ToList();
            return new Card(cardName, targetType, cardEffects);
        }
    }
}