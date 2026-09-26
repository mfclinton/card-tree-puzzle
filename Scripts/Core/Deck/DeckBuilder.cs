using System.Collections.Generic;
using CardGame.Core.Cards.Base;

namespace CardGame.Core.Deck
{
    public class DeckBuilder : IDeckBuilder
    {
        public List<Card> BuildStartingDeck(DeckConfig config)
        {
            var deck = new List<Card>();
            foreach (var (cardTemplate, count) in config.CardCounts)
            {
                for (int i = 0; i < count; i++)
                {
                    // Copy
                    var card = cardTemplate.Clone();
                    deck.Add(card);
                }
            }
            return deck;
        }
    }
}