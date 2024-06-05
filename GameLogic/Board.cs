using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class Board<T>
    {
        private Card<T>[,] cards;
        private int rows;
        private int columns;

        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }

        public Board(int rows, int columns, List<T> cardValues)
        {
            this.Rows = rows;
            this.Columns = columns;
            cards = new Card<T>[rows, columns];
            InitializeBoard(cardValues);
        }

        public static bool IsValidBoard(int rows, int columns)
        {
            return (rows * columns) % 2 == 0;
        }

        private void InitializeBoard(List<T> cardValues)
        {
            Random random = new Random();
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    int index = random.Next(cardValues.Count);
                    cards[row, col] = new Card<T>(row, col, cardValues[index]);
                    cardValues.RemoveAt(index);
                }
            }
        }

        public bool IsRevealed(int row, int col)
        {
            return cards[row, col].IsRevealed;
        }

        public T RevealCard(int row, int col)
        {
            cards[row, col].IsRevealed = true;
            return cards[row, col].Value;
        }

        public void HideCard(int row, int col)
        {
            cards[row, col].IsRevealed = false;
        }

        public bool AllCardsRevealed()
        {
            foreach (var card in cards)
            {
                if (!card.IsRevealed) return false;
            }
            return true;
        }

        public Card<T>[,] GetCards()
        {
            return cards;
        }
    }
}
