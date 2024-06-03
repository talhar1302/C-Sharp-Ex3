using GameLogic;
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
            string player1Name = ui.GetPlayerName("Enter player 1 name: ");
            string player2Name = null;
            if (ui.GetYesNoInput("Is this a two-player game? (yes/no): "))
            {
                player2Name = ui.GetPlayerName("Enter player 2 name: ");
            }

            GameLogic.Game game = new GameLogic.Game(rows, columns, player1Name, player2Name);
            while (!game.AllCardsRevealed())
            {
                ui.ClearScreen();
                ui.PrintBoard(game.GetBoard());
                ui.DisplayTurn(game.GetCurrentPlayer());
                (int row1, int col1) = ui.GetUserMove(game.GetBoard());
                game.RevealCard(row1, col1);
                ui.ClearScreen();
                ui.PrintBoard(game.GetBoard(), revealAll: false);
                ui.DisplayCard(game.GetBoard().GetCards()[row1, col1].Value, "First");
                (int row2, int col2) = ui.GetUserMove(game.GetBoard());
                game.RevealCard(row2, col2);
                ui.ClearScreen();
                ui.PrintBoard(game.GetBoard(), revealAll: false);
                ui.DisplayCards(game.GetBoard().GetCards()[row1, col1].Value, game.GetBoard().GetCards()[row2, col2].Value);
                if (game.CheckMatch(row1, col1, row2, col2))
                {
                    game.IncrementCurrentPlayerScore();
                    ui.DisplayMatch();
                }
                else
                {
                    System.Threading.Thread.Sleep(2000); // Wait for 2 seconds
                    ui.ClearScreen();
                    game.HideCards(row1, col1, row2, col2);
                    ui.PrintBoard(game.GetBoard());
                    ui.DisplayNoMatch();
                }
                game.NextPlayer();
            }

            ui.DisplayGameOver();
            if (player2Name != null)
            {
                if (game.GetPlayer1().Score > game.GetPlayer2().Score)
                {
                    ui.DisplayWinner(game.GetPlayer1());
                }
                else if (game.GetPlayer1().Score < game.GetPlayer2().Score)
                {
                    ui.DisplayWinner(game.GetPlayer2());
                }
                else
                {
                    ui.DisplayTie();
                }
            }
            else
            {
                ui.DisplayWinner(game.GetPlayer1());
            }
        }
    }
}
