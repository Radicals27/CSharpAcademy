/*
This app allows you to create stacks of flashcards
to test your knowledge on a subject
*/
namespace flashcard_app
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

            bool quitApp = false;

            while (quitApp == false)
            {
                View.DisplayMainMenuOptions();

                string? command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        quitApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        View.ShowStacksMenu(DBController.GetAllStacks());
                        HandleStackMenuSelection();
                        break;
                    case "2":
                        //DBController.Insert();
                        break;
                    case "3":
                        // DBController.Delete();
                        break;
                    case "4":
                        //DBController.Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }

        static void HandleStackMenuSelection()
        {
            int? userSelection = UserInput.GetNumberInput("Type the stack ID to edit one, or press 0 to make a new stack.");

            if (userSelection == 0)
            {
                // Make a new stack
                string newStackName = UserInput.GetStringInput("Enter the name of the new stack, or press 0 to return to main menu.");
                DBController.AddNewStack(newStackName);

                // Allow user to add flashcards to this stack
                View.CreateFlashcardForStackMenu(newStackName);
            }
            else
            {
                // Display all flashcards in this stack
                List<Flashcard> flashcardsInStack = DBController.GetFlashcardsByStackId(userSelection);

                foreach (Flashcard flashcard in flashcardsInStack)
                {
                    Console.Write($"{flashcard.Id}. {flashcard.FrontText}");
                }
            }
        }
    }
}