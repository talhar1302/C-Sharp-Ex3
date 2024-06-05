using GameLogic;
using System;
using System.Collections.Generic;

namespace GameUI
{
    using GameLogic;
    public class GameRunner
    {
        private UI ui;

        public GameRunner(UI ui)
        {
            this.ui = ui;
        }

        public void Run()
        {
            int rows, columns;
            (rows, columns) = ui.GetBoardSize();
            string player1Name = ui.GetPlayerName("Enter player 1 name: ");
            string player2Name = "Computer";
            Player currentPlayer;
            if (ui.GetYesNoInput("Is this a two-player game? (yes/no): "))
            {
                player2Name = ui.GetPlayerName("Enter player 2 name: ");
            }

            List<char> cardValues = ui.GenerateCharacterList(rows, columns);

            Game<char> game = new Game<char>(rows, columns, player1Name, player2Name, cardValues);
            while (!game.AllCardsRevealed())
            {
                ui.ClearScreen();
                ui.PrintBoard(game.GetBoard());
                currentPlayer = game.GetCurrentPlayer();
                ui.DisplayTurn(currentPlayer);

                int row1,col1;
                int row2,col2;

                if (game.GetCurrentPlayer() is ComputerPlayer<char>)
                {
                    // Computer player turn
                    (row1, col1) = game.GetComputerMove();
                    game.RevealCard(row1, col1);
                    ui.ClearScreen();
                    ui.PrintBoard(game.GetBoard(), revealAll: false);
                    ui.DisplayTurn(currentPlayer);
                    ui.DisplayCard(game.GetBoard().GetCards()[row1, col1].Value, "First");

                    System.Threading.Thread.Sleep(2000); // Wait for 2 seconds

                    (row2, col2) = game.GetComputerMove();
                    game.RevealCard(row2, col2);
                    ui.ClearScreen();
                    ui.PrintBoard(game.GetBoard(), revealAll: false);
                    ui.DisplayTurn(currentPlayer);
                    ui.DisplayCard(game.GetBoard().GetCards()[row2, col2].Value, "Second");

                    System.Threading.Thread.Sleep(2000); // Wait for 2 seconds
                }
                else
                {
                    // Human player turn
                    (row1, col1) = ui.GetUserMove(game.GetBoard());
                    game.RevealCard(row1, col1);
                    ui.ClearScreen();
                    ui.PrintBoard(game.GetBoard(), revealAll: false);
                    ui.DisplayTurn(currentPlayer);
                    ui.DisplayCard(game.GetBoard().GetCards()[row1, col1].Value, "First");

                    (row2, col2) = ui.GetUserMove(game.GetBoard());
                    game.RevealCard(row2, col2);
                    ui.ClearScreen();
                    ui.PrintBoard(game.GetBoard(), revealAll: false);
                    ui.DisplayTurn(currentPlayer);
                    ui.DisplayCard(game.GetBoard().GetCards()[row2, col2].Value, "Second");
                }

                if (game.CheckMatch(row1, col1, row2, col2))
                {
                    ui.DisplayMatch();
                    currentPlayer.IncreaseScore();
                }
                else
                {
                    ui.DisplayNoMatch();
                    System.Threading.Thread.Sleep(2000); // Wait for 2 seconds
                    game.HideCards(row1, col1, row2, col2);
                    ui.ClearScreen();
                    game.SwitchPlayer();
                }
            }

            Player winner = game.DetermineWinner();
            if (winner != null)
            {
                ui.DisplayWinner(winner);
            }
            else
            {
                ui.DisplayTie();
            }

            ui.DisplayGameOver();
        }
    }
}
