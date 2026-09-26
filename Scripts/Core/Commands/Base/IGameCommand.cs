using CardGame.Core.State;

namespace CardGame.Core.Commands.Base
{
    public interface IGameCommand
    {
        void Execute(GameState state);
    }
} 