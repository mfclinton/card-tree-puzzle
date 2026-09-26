using System.Collections.Generic;
using CardGame.Core.Cards.Base;

namespace CardGame.Core.Deck
{
    public interface IDeckBuilder
    {
        List<Card> BuildStartingDeck(DeckConfig config);
    }
}