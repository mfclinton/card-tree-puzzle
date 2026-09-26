using System;
using System.Linq;
using CardGame.Core.Tree.Base;
using CardGame.Core.Tree.Enums;
using CardGame.Core.Tree.Generator.Interfaces;
using CardGame.Core.Tree.Generator.Setting;
using CardGame.Core.Tree.Generator.Settings;

namespace CardGame.Core.Tree.Generator
{
    public class TreeGenerator : ITreeGenerator
    {
        private readonly Random _random = new();

        public TreeNode GenerateTree(GenerationConfig config)
        {
            config.Validate();
            return GenerateNodes(config, 0);
        }

        private TreeNode GenerateNodes(GenerationConfig config, int currentDepth)
        {
            if (currentDepth >= config.MaxDepth)
                return new TreeNode(NodeType.Exit);

            var selectedConfig = SelectNodeTypeConfig(config);
            if (currentDepth == 0)
                selectedConfig = config.NodeTypes.First(t => t.Type == NodeType.Normal);
            
            var childCount = SelectChildCount(selectedConfig);

            // Create node
            var node = new TreeNode(selectedConfig.Type);

            // Generate children
            for (int i = 0; i < childCount; i++)
            {
                var child = GenerateNodes(config, currentDepth + 1);
                node.AddChild(child);
            }

            return node;
        }

        #region Helper Methods

        private NodeTypeConfig SelectNodeTypeConfig(GenerationConfig config)
        {
            float cumulative = 0;
            NodeTypeConfig selectedConfig = null;

            float roll = (float)_random.NextDouble();
            foreach (var nodeConfig in config.NodeTypes)
            {
                cumulative += nodeConfig.Probability;
                if (roll <= cumulative)
                {
                    selectedConfig = nodeConfig;
                    break;
                }
            }

            return selectedConfig;
        }

        private int SelectChildCount(NodeTypeConfig config)
        {
            float cumulative = 0f;
            int childCount = 0;

            float roll = (float)_random.NextDouble();
            foreach (var distribution in config.ChildDistributions)
            {
                cumulative += distribution.Probability;
                if (roll <= cumulative)
                {
                    childCount = distribution.ChildCount;
                    break;
                }
            }

            return childCount;
        }

        #endregion
    }
}