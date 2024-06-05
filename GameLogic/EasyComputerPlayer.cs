namespace GameLogic
{
    public class EasyComputerPlayer<T> : ComputerPlayer<T>
    {
        public EasyComputerPlayer(string name) : base(name) { }

        public override (int, int) GetMove(Board<T> board)
        {
            int row, col;
            do
            {
                row = random.Next(board.Rows);
                col = random.Next(board.Columns);
            } while (board.IsRevealed(row, col));

            return (row, col);
        }
    }
}
