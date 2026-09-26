using CardGame.Core.Commands.Base;
using CardGame.Core.State;
using CardGame.Core.Tree;
using CardGame.Core.Tree.Base;

namespace CardGame.Core.Commands.Implementations
{
    public class SelectNodeCommand : IGameCommand
    {
        private readonly TreeNode _node;
        
        public SelectNodeCommand(TreeNode node)
        {
            _node = node;
        }

        public void Execute(GameState state)
        {
            state.SelectNode(_node);
        }
    }
}