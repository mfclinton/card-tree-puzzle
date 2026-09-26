using UnityEngine;
using System.Collections.Generic;
using CardGame.Core.Deck;

namespace CardGame.Unity.ScriptableObjects.Cards
{
    [CreateAssetMenu(fileName = "DeckConfig", menuName = "CardGame/DeckConfig")]
    public class DeckConfigSO : ScriptableObject
    {
        [System.Serializable]
        public class CardEntry
        {
            public CardConfigSO card;
            public int count;
        }

        public List<CardEntry> cards = new();
        
        public DeckConfig CreateDeckConfig()
        {
            var config = new DeckConfig();
            
            foreach (var entry in cards)
            {
                var card = entry.card.CreateCard();
                config.AddCard(card, entry.count);
            }
            
            return config;
        }
    }
} 