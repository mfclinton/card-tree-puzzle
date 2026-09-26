using System.Collections.Generic;
using CardGame.Core.Cards.Base;

namespace CardGame.Core.Deck
{
    public class DeckConfig
    {
        public Dictionary<Card, int> CardCounts { get; } = new();
        
        public void AddCard(Card card, int count)
        {
            CardCounts[card] = count;
        }
    }
}