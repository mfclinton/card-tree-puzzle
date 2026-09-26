using System;
using CardGame.Core.Cards.Effects.Implementations;

namespace CardGame.Unity.ScriptableObjects.Cards.Effects.Enums
{
    public enum ComparisonType
    {
        GreaterThan,
        LessThan,
        Equal
    }
    
    public static class ComparisonTypeExtensions
    {
        public static SubtreeComparison GetFunction(this ComparisonType comparisonType)
        {
            return comparisonType switch
            {
                ComparisonType.GreaterThan => (a, b) => a < b,
                ComparisonType.LessThan => (a, b) => a > b,
                ComparisonType.Equal => (a, b) => a == b,
                _ => throw new ArgumentException("Invalid comparison type")
            };
        }
    }
}