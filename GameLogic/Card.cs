namespace GameLogic
{
    public class Card
    {
        public char Value { get; }
        public bool IsRevealed { get; set; }

        public Card(char value)
        {
            Value = value;
            IsRevealed = false;
        }
    }
}
