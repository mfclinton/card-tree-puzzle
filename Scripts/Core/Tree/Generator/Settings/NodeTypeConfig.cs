using System;
using System.Linq;
using System.Collections.Generic;
using CardGame.Core.Tree.Enums;
using CardGame.Core.Tree.Generator.Settings;

namespace CardGame.Core.Tree.Generator.Setting
{
    [Serializable]
    public class NodeTypeConfig
    {
        public NodeType Type;
        public float Probability;
        public List<ChildDistribution> ChildDistributions = new();

        public void Validate()
        {
            if (Probability < 0 || Probability > 1)
                throw new ArgumentException($"Probability for {Type} must be between 0 and 1");
            
            if (!ChildDistributions.Any())
                throw new ArgumentException($"Node type {Type} must have at least one child distribution");
                
            if (Math.Abs(ChildDistributions.Sum(d => d.Probability) - 1f) > 0.001f)
                throw new ArgumentException($"Child distribution probabilities for {Type} must sum to 1");
        }
    }
}