/*
This app allows you to create cocktails quickly and easily via API calls
*/
using System.Diagnostics;
using Newtonsoft.Json;

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
            ShowMainMenu(Data.mainMenuOptions);
        }

        static async void ShowMainMenu(Dictionary<string, Func<Task>> options)
        {
            Console.Clear();

            View.DisplayMenu(options);

            int input = UserInput.GetNumberInput("Enter your choice: ");

            if (input >= 1 && input <= options.Count)
            {
                var action = GetActionByIndex(options, input - 1);

                if (action == ExitProgram)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please wait, retrieving...");

                    await action();
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Press Enter to try again.");
                Console.ReadKey();
            }
        }

        static Func<Task> GetActionByIndex(Dictionary<string, Func<Task>> options, int index)
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

        internal static async Task OrdinaryDrink()
        {
            string baseUrl = "https://www.thecocktaildb.com/api/json/v1/1/";
            string endpoint = "filter.php?c=Ordinary_Drink";

            using (HttpClient client = new HttpClient())
            {
                // Set the base address
                client.BaseAddress = new Uri(baseUrl);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var serialize = JsonConvert.DeserializeObject<Drinks>(responseData);
                        List<Drink> returnedDrinkList = serialize.DrinkList;

                        View.DisplayDrinksTable(returnedDrinkList);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        Console.WriteLine($"Message: {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred:");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        internal static async Task Cocktail()
        {

        }

        internal static async Task MilkFloatShake()
        {

        }

        internal static async Task OtherUnknown()
        {

        }

        internal static async Task Cocoa()
        {

        }

        internal static async Task Shot()
        {

        }

        internal static async Task CoffeeTea()
        {

        }

        internal static async Task HomemadeLiquer()
        {

        }

        internal static async Task PunchPartyDrink()
        {

        }

        internal static async Task Beer()
        {

        }

        internal static async Task Softdrink()
        {

        }

        internal static async Task ExitProgram()
        {
            quitApp = true;
        }

    }
}