using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public class Game<T>
    {
        private Board<T> board;
        private List<Player> players;
        private int currentPlayerIndex;

        public Game(int rows, int columns, List<string> playerNames, List<T> cardValues)
        {
            board = new Board<T>(rows, columns, cardValues);
            players = new List<Player>();

            foreach (var name in playerNames)
            {
                players.Add(name == "Computer" ? (Player)new AIComputerPlayer<T>(name) : new Player(name));
            }

            currentPlayerIndex = 0;
        }

        public Board<T> GetBoard()
        {
            return board;
        }

        public Player GetCurrentPlayer()
        {
            return players[currentPlayerIndex];
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

        public bool AllCardsRevealed()
        {
            return board.AllCardsRevealed();
        }

        public bool CheckMoveValidation(int row, int col)
        {
            if (row >= 0 && col >= 0 && row <= board.Rows - 1 && col <= board.Columns - 1)
            {
                return !board.IsRevealed(row, col);
            }
            return false;
        }

        public bool CheckMatch(int row1, int col1, int row2, int col2)
        {
            return board.GetCards()[row1, col1].Value.Equals(board.GetCards()[row2, col2].Value);
        }

        public void SwitchPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        public Player DetermineWinner()
        {
            return players.OrderByDescending(p => p.Score).FirstOrDefault();
        }

        public (int, int) GetComputerMove()
        {
            if (GetCurrentPlayer() is ComputerPlayer<T> computerPlayer)
            {
                return computerPlayer.GetMove(board);
            }
            throw new InvalidOperationException("Current player is not a computer player.");
        }
    }
}
