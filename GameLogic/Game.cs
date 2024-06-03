using System.Threading;

namespace GameLogic
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private Player currentPlayer;

        public Game(int rows, int columns, string player1Name, string player2Name = null)
        {
            board = new Board(rows, columns);
            player1 = new Player(player1Name);
            player2 = player2Name != null ? new Player(player2Name) : null;
            currentPlayer = player1;
        }

        public Board GetBoard()
        {
            return board;
        }

        public Player GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public Player GetPlayer1()
        {
            return player1;
        }

        public Player GetPlayer2()
        {
            return player2;
        }

        public void NextPlayer()
        {
            if (player2 != null)
            {
                currentPlayer = currentPlayer == player1 ? player2 : player1;
            }
        }

        public void RevealCard(int row, int col)
        {
            board.RevealCard(row, col);
        }

        public void HideCards(int row1, int col1, int row2, int col2)
        {
            board.HideCard(row1, col1);
            board.HideCard(row2, col2);
        }

        public bool CheckMatch(int row1, int col1, int row2, int col2)
        {
            return board.GetCards()[row1, col1].Value == board.GetCards()[row2, col2].Value;
        }

        public void IncrementCurrentPlayerScore()
        {
            currentPlayer.IncrementScore();
        }

        public bool AllCardsRevealed()
        {
            return board.AllCardsRevealed();
        }
    }
}
