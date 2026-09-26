using CardGame.Core.Deck;
using CardGame.Core.Tree.Generator.Settings;

namespace CardGame.Core.Game
{
    public class GameManagerConfig
    {
        public int InitialHandSize { get; set; } = 5;
        public GenerationConfig TreeConfig { get; set; }
        public DeckConfig DeckConfig { get; set; }
    }
}