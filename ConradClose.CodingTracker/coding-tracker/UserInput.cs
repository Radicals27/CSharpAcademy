using System.Globalization;

namespace coding_tracker
{
    /// <summary>
    /// Responsible for taking in all user input and validating it
    /// </summary>
    class UserInput
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("Welcome to the game-tracking app");
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\n1. View all records");
                Console.WriteLine("2. Insert a record");
                Console.WriteLine("3. Delete a record");
                Console.WriteLine("4. Update a record");
                Console.WriteLine("5. Get report for a year");
                Console.WriteLine("0. Exit");
                Console.WriteLine("------------------------------------------\n");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        DBController.GetAllRecords();
                        break;
                    case "2":
                        DBController.Insert();
                        break;
                    case "3":
                        DBController.Delete();
                        break;
                    case "4":
                        DBController.Update();
                        break;
                    case "5":
                        DBController.GetReportForAYear();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }

        public static string GetStringInput(string message)
        {
            Console.WriteLine(message);

            string stringInput = Console.ReadLine();

            if (stringInput == "0") ShowMainMenu();

            while (stringInput == null)
            {
                Console.WriteLine("\n\nInvalid input, please try again:");
                stringInput = Console.ReadLine();
            }

            return stringInput;
        }

        public static int GetTwoDigitYearFromUser()
        {
            int year = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Please enter a 2-digit year:");

                string input = Console.ReadLine();

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

            int fullYear = 2000 + year;

            return fullYear;
        }

        public static string GetDateInput()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main manu.\n\n");

            string dateInput = Console.ReadLine();

            if (dateInput == "0") ShowMainMenu();

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid date. (Format: dd-mm-yy). Type 0 to return to main manu or try again:\n\n");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }

        public static int GetNumberInput(string message)
        {
            Console.WriteLine(message);

            string numberInput = Console.ReadLine();

            if (numberInput == "0") ShowMainMenu();

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