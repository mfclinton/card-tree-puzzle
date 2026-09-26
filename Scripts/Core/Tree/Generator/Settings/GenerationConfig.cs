using System;
using System.Linq;
using System.Collections.Generic;
using CardGame.Core.Tree.Generator.Setting;

namespace CardGame.Core.Tree.Generator.Settings
{
    public class GenerationConfig
    {
        // Settings
        public int MaxDepth = 3;
        public List<NodeTypeConfig> NodeTypes = new();

        public void Validate()
        {
            if (MaxDepth < 1)
                throw new ArgumentException("MaxDepth must be at least 1");
            
            if (!NodeTypes.Any())
                throw new ArgumentException("Must define at least one node type");
                
            if (Math.Abs(NodeTypes.Sum(t => t.Probability) - 1f) > 0.001f)
                throw new ArgumentException("Node type probabilities must sum to 1");
                
            foreach (var nodeType in NodeTypes)
            {
                nodeType.Validate();
            }
        }
    }
}