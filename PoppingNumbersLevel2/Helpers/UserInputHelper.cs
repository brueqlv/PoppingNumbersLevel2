namespace PoppingNumbersLevel2.Helpers
{
    public static class UserInputHelper
    {
        public static int GetValidUserInputNumber(string message, int max, int min = 1)
        {
            while (true)
            {
                Console.Write($"{message} ({min}-{max}): ");
                var numberInput = Console.ReadLine();

                if (int.TryParse(numberInput, out var result))
                {
                    if (result >= min && result <= max)
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
