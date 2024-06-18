using PoppingNumbersLevel2.Helpers;
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

            var deskWidth = UserInputHelper.GetValidUserInputNumberFromTo("Enter board desk width", minDeskSize, maxDeskSize);
            var deskHeight = UserInputHelper.GetValidUserInputNumberFromTo("Enter board desk height", minDeskSize, maxDeskSize);

            var gameBoard = new GameBoard(deskWidth, deskHeight);
            var gameService = new GameService(gameBoard);

            var gameNumbersFrom = UserInputHelper.GetValidUserInputNumberFromTo("Enter min game number", minGameNumber, maxGameNumber);
            var gameNumbersTo = UserInputHelper.GetValidUserInputNumberFromTo("Enter max game number", minGameNumber, maxGameNumber);

            var gameNumbers = new GameNumbers(gameNumbersFrom, gameNumbersTo);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{userName} score - {score}");

                gameService.PrintBoard();

                var number = UserInputHelper.GetValidUserInputNumberFromTo("Enter a number", gameNumbers.From, gameNumbers.To);
                var row = UserInputHelper.GetValidUserInputTo("row", gameBoard.Height);
                var col = UserInputHelper.GetValidUserInputTo("col", gameBoard.Width);

                gameService.PlayerTurn(number, row, col);

                if (gameService.IsGameOver())
                {
                    break;
                }

                gameService.ComputerTurn(minGameNumber, maxGameNumber);

                score += gameService.ClearConnectedNumbers();

                if (gameService.IsGameOver())
                {
                    break;
                }
            }
        }
    }
}
