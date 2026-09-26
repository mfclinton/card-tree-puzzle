using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CardGame.Core.Cards.Base;
using CardGame.Core.Tree.Base;

namespace System.Runtime.CompilerServices { internal static class IsExternalInit {} }

namespace CardGame.Core.Events.Events
{
    public class GameStateEvents
    {
        public record CardDrawnEvent(Card Card);
        public record CardDiscardedEvent(Card Card);
        public record NodeEnteredEvent(TreeNode Node);
        public record CardSelectedEvent(Card Card, IEnumerable<TreeNode> ValidTargets);
        public record InformationGainedEvent(InformationResult Information);
    }
}