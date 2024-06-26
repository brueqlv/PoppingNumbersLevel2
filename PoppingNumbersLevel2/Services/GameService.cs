﻿using PoppingNumbersLevel2.Models;

namespace PoppingNumbersLevel2.Services
{
    public class GameService(GameBoard gameBoard)
    {
        private readonly Random _gameRandom = new();

        public void PrintBoard()
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                var sb1 = new List<string>();
                var lines = new List<string>();

                for (var j = 0; j < gameBoard.Width; j++)
                {
                    sb1.Add(gameBoard.Board[i, j] == null ? "   " : " " + gameBoard.Board[i, j] + " ");
                    lines.Add("---");
                }

                Console.WriteLine(string.Join("|", sb1));

                if (i < gameBoard.Height - 1)
                {
                    Console.WriteLine(string.Join("|", lines));
                }
            }
        }

        public void PlayerTurn(string number, int row, int col)
        {
            while (true)
            {
                if (gameBoard.Board[row - 1, col - 1] == null)
                {
                    gameBoard.Board[row - 1, col - 1] = number;
                    break;
                }

                Console.WriteLine("Field is already occupied, try again.");
            }
        }

        public void ComputerTurn(int minGameNumber, int maxGameNumber)
        {
            var numbersPlaced = 0;

            while (numbersPlaced < 3 || IsGameOver())
            {
                var row = _gameRandom.Next(gameBoard.Height);
                var col = _gameRandom.Next(gameBoard.Width);

                if (gameBoard.Board[row, col] == null)
                {
                    gameBoard.Board[row, col] = _gameRandom.Next(minGameNumber, maxGameNumber + 1).ToString();
                    numbersPlaced++;
                }
            }
        }

        public bool IsGameOver()
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (gameBoard.Board[i, j] == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int ClearConnectedNumbers()
        {
            var toClear = new bool[gameBoard.Height, gameBoard.Width];

            CheckConnections(toClear, 0, 1);  // Horizontal
            CheckConnections(toClear, 1, 0);  // Vertical
            CheckConnections(toClear, 1, 1);  // Diagonal
            CheckConnections(toClear, 1, -1); // Reverse Diagonal

            return ClearMarkedCells(toClear);
        }

        private void CheckConnections(bool[,] toClear, int rowIncrement, int colIncrement)
        {
            var height = gameBoard.Height;
            var width = gameBoard.Width;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    MarkConnectedCells(toClear, i, j, rowIncrement, colIncrement);
                }
            }
        }

        private void MarkConnectedCells(bool[,] toClear, int startRow, int startCol, int rowIncrement, int colIncrement)
        {
            var current = gameBoard.Board[startRow, startCol];
            if (current == null) return;

            var count = 1;
            var row = startRow + rowIncrement;
            var col = startCol + colIncrement;

            while (IsValidPosition(row, col) && gameBoard.Board[row, col] == current)
            {
                count++;
                row += rowIncrement;
                col += colIncrement;
            }

            if (count >= 3)
            {
                for (var k = 0; k < count; k++)
                {
                    toClear[startRow + k * rowIncrement, startCol + k * colIncrement] = true;
                }
            }
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < gameBoard.Height && col >= 0 && col < gameBoard.Width;
        }

        private int ClearMarkedCells(bool[,] toClear)
        {
            var clearedCells = 0;
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (toClear[i, j])
                    {
                        gameBoard.Board[i, j] = null;
                        clearedCells++;
                    }
                }
            }

            return clearedCells;
        }
    }
}
