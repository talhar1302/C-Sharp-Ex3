using System;

namespace GameLogic
{
    public class ComputerPlayer<T> : Player
    {
        private Random random;

        public ComputerPlayer(string name) : base(name)
        {
            random = new Random();
        }

        public (int, int) GetRandomMove(Board<T> board)
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
