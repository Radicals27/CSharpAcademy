using System.Globalization;

namespace coding_tracker
{
    /// <summary>
    /// Responsible for taking in all user input and validating it
    /// </summary>
    class UserInput
    {
        internal static string? GetStringInput(string message)
        {
            Console.WriteLine(message);

            string? stringInput = Console.ReadLine();

            if (stringInput == "0") return null;

            while (stringInput == null)
            {
                Console.WriteLine("\n\nInvalid input, please try again:");
                stringInput = Console.ReadLine();
            }

            return stringInput;
        }

        internal static int GetTwoDigitYearFromUser()
        {
            int year = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Please enter a 2-digit year:");

                string? input = Console.ReadLine();

                if (input == null)
                {
                    continue;
                }

                if (input.Length == 2 && input.All(char.IsDigit))
                {
                    year = Convert.ToInt32(input);

                    if (year >= 0 && year <= 99)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid 2-digit year (00-99).");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a 2-digit year.");
                }
            }

            int fullYear;
            int currentYear = DateTime.Now.Year % 100;

            if (year > currentYear)
            {
                // If the year is "25" when the current year is actually 2024, it must be 1925
                fullYear = 1900 + year;
            }
            else
            {
                fullYear = 2000 + year;
            }
            return fullYear;
        }

        internal static string? GetDateInput()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main manu.\n\n");

            string? dateInput = Console.ReadLine();

            if (dateInput == "0") return null;

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid date. (Format: dd-mm-yy). Type 0 to return to main manu or try again:\n\n");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }

        internal static int? GetNumberInput(string message)
        {
            Console.WriteLine(message);

            string? numberInput = Console.ReadLine();

            if (numberInput == "0") return null;

            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("\n\nInvalid number. Try again.\n\n");
                numberInput = Console.ReadLine();
            }

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }
    }
}