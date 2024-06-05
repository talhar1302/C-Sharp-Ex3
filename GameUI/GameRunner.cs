using GameLogic;
using System;
using System.Collections.Generic;

namespace GameUI
{
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
            List<string> playerNames = new List<string> { ui.GetPlayerName("Enter player 1 name: ") };

            if (ui.GetYesNoInput("Is this a two-player game? (yes/no): "))
            {
                playerNames.Add(ui.GetPlayerName($"Enter player {playerNames.Count + 1} name: "));
            }
            else
            {
                playerNames.Add("Computer");
            }

            List<char> cardValues = ui.GenerateCharacterList(rows, columns);
            Game<char> game = new Game<char>(rows, columns, playerNames, cardValues);

            while (!game.AllCardsRevealed())
            {
                ui.ClearScreen();
                ui.PrintBoard(game.GetBoard());
                ui.DisplayTurn(game.GetCurrentPlayer());

                (int row1, int col1) = ui.GetMove(game);
                game.RevealCard(row1, col1);
                ui.DisplayBoardAndCard(game, row1, col1, "First");

                (int row2, int col2) = ui.GetMove(game);
                game.RevealCard(row2, col2);
                ui.DisplayBoardAndCard(game, row2, col2, "Second");

                if (game.CheckMatch(row1, col1, row2, col2))
                {
                    ui.DisplayMatch();
                    game.GetCurrentPlayer().IncreaseScore();
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

            ui.DisplayWinnerOrTie(game);
            ui.DisplayGameOver();
        }
    }
}
