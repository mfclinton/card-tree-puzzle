using CardGame.Core.Tree.Enums;

namespace CardGame.Core.Tree.Generator.Settings
{
    public class NodeSpawnConfig
    {
        public NodeType Type { get; }
        public float Probability { get; }

        public NodeSpawnConfig(NodeType type, float probability)
        {
            Type = type;
            Probability = probability;
        }
    }
}