using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI
{
    using GameLogic;
    using System;
    using Ex02.ConsoleUtils;
    public class UI
    {
        public void ClearScreen()
        {
            Screen.Clear();
        }

        public string GetPlayerName(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public bool GetYesNoInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().ToLower() == "yes";
        }

        public (int, int) GetBoardSize()
        {
            int rows, columns;
            while (true)
            {
                Console.Write("Enter the number of rows (4,5,6): ");
                rows = int.Parse(Console.ReadLine());
                Console.Write("Enter the number of columns (4,5,6): ");
                columns = int.Parse(Console.ReadLine());

                if ((rows >= 4 && rows <= 6) && (columns >= 4 && columns <= 6) && Board<char>.IsValidBoard(rows, columns))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid board size. Please try again.");
                }
            }
            return (rows, columns);
        }

        public void PrintBoard(Board<char> board, bool revealAll = false)
        {
            Card<char>[,] cards = board.GetCards();
            int rows = cards.GetLength(0);
            int columns = cards.GetLength(1);

            // Print the column headers
            Console.Write("  ");
            for (int c = 0; c < columns; c++)
            {
                Console.Write((char)('A' + c) + " ");
            }
            Console.WriteLine();

            // Print the top border
            Console.Write(" ");
            for (int c = 0; c < columns; c++)
            {
                Console.Write("==");
            }
            Console.Write("=");
            Console.WriteLine();

            // Print the board rows
            for (int i = 0; i < rows; i++)
            {
                Console.Write((i + 1).ToString() + "|");
                for (int j = 0; j < columns; j++)
                {
                    if (cards[i, j].IsRevealed || revealAll)
                    {
                        Console.Write(cards[i, j].Value);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
                // Print the top border
                Console.Write(" ");
                for (int c = 0; c < columns; c++)
                {
                    Console.Write("==");
                }
                Console.Write("=");
                Console.WriteLine();
            }
        }


        public void DisplayTurn(Player player)
        {
            Console.WriteLine($"{player.Name}'s turn. Score: {player.Score}");
        }

        public void DisplayCard(char card, string cardOrder)
        {
            Console.WriteLine($"{cardOrder} card: {card}");
        }

        public void DisplayCards(char firstCard, char secondCard)
        {
            Console.WriteLine($"First card: {firstCard}, Second card: {secondCard}");
        }

        public void DisplayMatch()
        {
            Console.WriteLine("It's a match! You get another turn.");
        }

        public void DisplayNoMatch()
        {
            Console.WriteLine("Not a match.");
        }

        public void DisplayWinner(Player player)
        {
            Console.WriteLine($"{player.Name} wins with {player.Score} points!");
        }

        public void DisplayTie()
        {
            Console.WriteLine("It's a tie!");
        }

        public void DisplayGameOver()
        {
            Console.WriteLine("Game over. Thank you for playing!");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        public (int, int) GetUserMove(Game<char> game)
        {
            while (true)
            {
                Console.Write("Enter a card to reveal (e.g., A1 or Q to quit): ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "Q")
                {
                    Environment.Exit(0);
                }

                (int row, int col) = ParseInput(input);
                if (CheckMoveValidation(game, row, col))
                {
                    return (row, col);
                }
                Console.WriteLine("Invalid input or card already revealed. Try again.\n");
            }
        }

        private bool CheckMoveValidation(Game<char>i_game, int i_row, int i_column)
        {
            Board<char> board = i_game.GetBoard();
            return i_game.CheckMoveValidation(i_row, i_column);
        }
        private (int, int) ParseInput(string input)
        {
            if (input.Length == 2 && char.IsLetter(input[0]) && char.IsDigit(input[1]))
            {
                int col = char.ToUpper(input[0]) - 'A';
                int row = int.Parse(input[1].ToString()) - 1;
                return (row, col);
            }
            return (-1, -1);
        }

        public List<char> GenerateCharacterList(int rows, int columns)
        {
            List<char> cardValues = new List<char>();
            char value = 'A';
            for (int i = 0; i < (rows * columns) / 2; i++)
            {
                cardValues.Add(value);
                cardValues.Add(value);
                value++;
            }
            return cardValues;
        }

        public (int, int) GetMove(Game<char> game)
        {
            if (game.GetCurrentPlayer() is ComputerPlayer<char>)
            {
                return game.GetComputerMove();
            }
            return GetUserMove(game);
        }

        public void DisplayBoardAndCard(Game<char> game, int row, int col, string cardOrder)
        {
            ClearScreen();
            PrintBoard(game.GetBoard(), revealAll: false);
            Player currentPlayer = game.GetCurrentPlayer();
            DisplayTurn(currentPlayer);
            DisplayCard(game.GetBoard().GetCards()[row, col].Value, cardOrder);
            if(game.GetCurrentPlayer() is ComputerPlayer<char>)
            System.Threading.Thread.Sleep(2000); // Wait for 2 seconds
        }

        public void DisplayWinnerOrTie(Game<char> game)
        {
            Player winner = game.DetermineWinner();
            if (winner != null)
            {
                DisplayWinner(winner);
            }
            else
            {
                DisplayTie();
            }
        }
    }
}
