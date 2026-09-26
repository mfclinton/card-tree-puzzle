using System;

namespace CardGame.Core.Tree.Generator.Settings
{
    [Serializable]
    public class ChildDistribution
    {
        public int ChildCount;
        public float Probability;

        public ChildDistribution(int childCount, float probability)
        {
            ChildCount = childCount;
            Probability = probability;
        }
    }
} 