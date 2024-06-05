using GameLogic;
using System;
using System.Collections.Generic;

namespace GameUI
{
    public class Game<T>
    {
        private Board<T> board;
        private Player player1;
        private Player player2;
        private Player currentPlayer;

        public Game(int rows, int columns, string player1Name, string player2Name, List<T> cardValues)
        {
            board = new Board<T>(rows, columns, cardValues);
            player1 = new Player(player1Name);
            player2 = player2Name == "Computer" ? (Player)new ComputerPlayer<T>(player2Name) : new Player(player2Name);
            currentPlayer = player1;
        }

        public Board<T> GetBoard()
        {
            return board;
        }

        public Player GetCurrentPlayer()
        {
            return currentPlayer;
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

        public bool CheckMatch(int row1, int col1, int row2, int col2)
        {
            return board.GetCards()[row1, col1].Value.Equals(board.GetCards()[row2, col2].Value);
        }

        public void SwitchPlayer()
        {
            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }

        public Player DetermineWinner()
        {
            if (player1.Score > player2.Score)
            {
                return player1;
            }
            else if (player2.Score > player1.Score)
            {
                return player2;
            }
            return null; // It's a tie
        }

        public (int, int) GetComputerMove()
        {
            if (currentPlayer is ComputerPlayer<T> computerPlayer)
            {
                return computerPlayer.GetRandomMove(board);
            }
            throw new InvalidOperationException("Current player is not a computer player.");
        }
    }
}
