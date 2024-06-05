namespace GameLogic
{
    public class Card<T>
    {
        public enum CardType
        {
            CHAR
        }
        public T Value { get; }
        public bool IsRevealed { get; set; }

        public Card(T value)
        {
            Value = value;
            IsRevealed = false;
        }
    }
}
