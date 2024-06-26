﻿using PoppingNumbersLevel2.Helpers;
using PoppingNumbersLevel2.Models;
using PoppingNumbersLevel2.Services;

namespace PoppingNumbersLevel2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int minDeskSize = 5;
            const int maxDeskSize = 25;
            const int minGameNumber = 0;
            const int maxGameNumber = 9;
            var score = 0;

            var userName = UserInputHelper.GetValidUserName();

            var deskWidth = UserInputHelper.GetValidUserInputNumber("Enter board desk width", maxDeskSize, minDeskSize);
            var deskHeight = UserInputHelper.GetValidUserInputNumber("Enter board desk height", maxDeskSize, minDeskSize);

            var gameBoard = new GameBoard(deskWidth, deskHeight);
            var gameService = new GameService(gameBoard);

            var gameNumbersFrom = UserInputHelper.GetValidUserInputNumber("Enter min game number", maxGameNumber, minGameNumber);
            var gameNumbersTo = UserInputHelper.GetValidUserInputNumber("Enter max game number", maxGameNumber, minGameNumber);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{userName} score - {score}");

                gameService.PrintBoard();

                var number = UserInputHelper.GetValidUserInputNumber("Enter a number", gameNumbersTo, gameNumbersFrom).ToString();
                var row = UserInputHelper.GetValidUserInputNumber("Enter row", gameBoard.Height);
                var col = UserInputHelper.GetValidUserInputNumber("Enter col", gameBoard.Width);

                gameService.PlayerTurn(number, row, col);

                if (gameService.IsGameOver())
                {
                    break;
                }

                gameService.ComputerTurn(gameNumbersFrom, gameNumbersTo);

                score += gameService.ClearConnectedNumbers();

                if (gameService.IsGameOver())
                {
                    break;
                }
            }

            Console.WriteLine("Game Over! No more spaces left.");
            Console.ReadLine();
        }
    }
}
