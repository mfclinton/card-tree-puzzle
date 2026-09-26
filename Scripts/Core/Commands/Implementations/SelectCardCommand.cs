using System.Collections.Generic;
using CardGame.Core.Cards.Base;
using CardGame.Core.Cards.Effects.Interfaces;
using CardGame.Core.Commands.Base;
using CardGame.Core.Events;
using CardGame.Core.Events.Events;
using CardGame.Core.State;
using CardGame.Core.Tree.Base;

namespace CardGame.Core.Commands.Implementations
{
    public class SelectCardCommand : IGameCommand
    {
        private readonly Card _card;

        public SelectCardCommand(Card card)
        {
            _card = card;
        }

        public void Execute(GameState state)
        {
            state.SelectCard(_card);
        }
    }
} 