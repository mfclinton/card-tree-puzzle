using CardGame.Core.Cards.Base;
using CardGame.Core.DataStructures;
using CardGame.Unity.Cards.Visuals;

namespace CardGame.Unity.Registries
{
    public static class CardRegistry
    {
        public static readonly Map<Card, CardVisual> CardVisualMap = new();
    }
}