
namespace PoppingNumbersLevel2.Helpers
{
    public static class UserInputHelper
    {
        public static int GetValidUserInputNumberFromTo(string message, int from, int to)
        {
            while (true)
            {
                Console.Write($"{message} ({from}-{to}): ");
                var numberInput = Console.ReadLine();

                if (int.TryParse(numberInput, out var result))
                {
                    if (result >= from && result <= to)
                    {
                        return result;
                    }
                }

                Console.WriteLine("Invalid input, try again.");
            }
        }

        public static int GetValidUserInputTo(string message, int max)
        {
            while (true)
            {
                Console.Write($"Enter {message} (1 - {max}): ");
                var numberInput = Console.ReadLine();

                if (int.TryParse(numberInput, out var result))
                {
                    if (result >= 1 && result <= max)
                    {
                        return result;
                    }
                }

                Console.WriteLine("Invalid input, try again.");
            }
        }

        internal static object GetValidUserName()
        {
            while (true)
            {
                Console.WriteLine("Please enter your name.");
                var userName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    return userName;
                }
            }
        }
    }
}
