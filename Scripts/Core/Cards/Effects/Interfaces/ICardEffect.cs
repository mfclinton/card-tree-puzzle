using CardGame.Core.State;
using CardGame.Core.Tree.Base;

namespace CardGame.Core.Cards.Effects.Interfaces
{
    public interface ICardEffect
    {
        bool ValidateTarget(TreeNode target, GameState state);
        string GetDescription();
    }
}