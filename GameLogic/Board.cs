using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class Board
    {
        private Card[,] cards;
        private int rows;
        private int columns;

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            cards = new Card[rows, columns];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            List<char> cardValues = new List<char>();
            char value = 'A';
            for (int i = 0; i < (rows * columns) / 2; i++)
            {
                cardValues.Add(value);
                cardValues.Add(value);
                value++;
            }

            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = random.Next(cardValues.Count);
                    cards[i, j] = new Card(cardValues[index]);
                    cardValues.RemoveAt(index);
                }
            }
        }

        public bool IsRevealed(int row, int col)
        {
            return cards[row, col].IsRevealed;
        }

        public char RevealCard(int row, int col)
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

        public Card[,] GetCards()
        {
            return cards;
        }
    }
}
