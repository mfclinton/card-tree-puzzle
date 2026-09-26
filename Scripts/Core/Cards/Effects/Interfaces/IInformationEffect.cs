using CardGame.Core.Cards.Base;
using CardGame.Core.State;
using CardGame.Core.Tree.Base;

namespace CardGame.Core.Cards.Effects.Interfaces
{
    public interface IInformationEffect : ICardEffect
    {
        InformationResult GetInformation(TreeNode target, GameState state);
    }
}