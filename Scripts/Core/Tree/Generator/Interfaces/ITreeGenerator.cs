using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Generator.Settings;

namespace CardGame.Core.Tree.Generator.Interfaces
{
    public interface ITreeGenerator
    {
        TreeNode GenerateTree(GenerationConfig config);
    }
}