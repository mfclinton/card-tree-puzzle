using CardGame.Core.Cards.Enums;

namespace CardGame.Core.Cards.Base
{
    public class InformationResult
    {
        public InformationType Type { get; }
        public object Value { get; }

        public InformationResult(InformationType type, object value)
        {
            Type = type;
            Value = value;
        }
    }
}