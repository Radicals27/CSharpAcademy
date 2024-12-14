/*
This app allows you to create cocktails quickly and easily via API calls
*/
using System.Diagnostics;

namespace cocktails
{
    /// <summary>
    /// Responsible for the main app flow
    /// </summary>
    class Program
    {
        static bool quitApp = false;

        static void Main(string[] args)
        {
            MainMenuLoop();
        }

        /// <summary>
        /// Handles the core loop of showing the user the main menu and accepting input
        /// </summary>
        static void MainMenuLoop()
        {
            Console.Clear();

            while (quitApp == false)
            {
                HandleMainMenuSelection();
            }
        }

        private static void HandleMainMenuSelection()
        {
            var mainMenuOptions = new Dictionary<string, Action>
            {
                { "Ordinary Drink", OrdinaryDrink },
                { "Cocktail", Cocktail },
                { "Milk / Float / Shake", MilkFloatShake },
                { "Other / Unknown", OtherUnknown },
                { "Cocoa", Cocoa },
                { "Shot", Shot },
                { "Coffee / Tea", CoffeeTea },
                { "Homemade Liquer", HomemadeLiquer },
                { "Punch / Party Drink", PunchPartyDrink },
                { "Beer", Beer },
                { "Softdrink / Soda", Softdrink },
                { "Return to main menu.", ExitProgram }
            };

            RunMenu(mainMenuOptions);
        }

        static void RunMenu(Dictionary<string, Action> options)
        {
            while (true)
            {
                Console.Clear();

                View.DisplayMenu(options);

                int input = UserInput.GetNumberInput("Enter your choice: ");

                if (input >= 1 && input <= options.Count)
                {
                    var action = GetActionByIndex(options, input - 1);
                    action.Invoke();

                    if (action == ExitProgram)
                        break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                }
            }
        }

        static Action GetActionByIndex(Dictionary<string, Action> options, int index)
        {
            int i = 0;

            foreach (var action in options.Values)
            {
                if (i == index)
                    return action;
                i++;
            }

            return null;
        }

        private static void OrdinaryDrink()
        {

        }

        private static void Cocktail()
        {

        }

        private static void MilkFloatShake()
        {

        }

        private static void OtherUnknown()
        {

        }

        private static void Cocoa()
        {

        }

        private static void Shot()
        {

        }

        private static void CoffeeTea()
        {

        }

        private static void HomemadeLiquer()
        {

        }

        private static void PunchPartyDrink()
        {

        }

        private static void Beer()
        {

        }

        private static void Softdrink()
        {

        }

        private static void ExitProgram()
        {
            quitApp = true;
        }
    }
}