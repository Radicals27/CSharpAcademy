
/*
This app allows you to track your hours-played in terms of 
computer and video games, to help you make better habits.
*/
namespace coding_tracker
{
    /// <summary>
    /// Responsible for the main app flow
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DBController.InitialiseDB();
            AppFlow();
        }

        /// <summary>
        /// Handles the core loop of showing the user the main menu and accepting input
        /// </summary>
        static void AppFlow()
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

                string? command = Console.ReadLine();

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
    }
}