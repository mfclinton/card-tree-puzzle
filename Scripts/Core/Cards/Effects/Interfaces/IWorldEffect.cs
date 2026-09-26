using CardGame.Core.State;
using CardGame.Core.Tree.Base;

namespace CardGame.Core.Cards.Effects.Interfaces
{
    public interface IWorldEffect : ICardEffect
    {
        void ApplyEffect(TreeNode target, GameState state);
    }
}