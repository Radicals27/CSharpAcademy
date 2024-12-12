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
                        View.ShowStacksMainMenu(DBController.GetAllStacks());
                        HandleStackMainMenuSelection();
                        break;
                    case "2":
                        View.ShowFlashcardMainMenu(DBController.GetFlashcardsByStackId(1));
                        HandleFlashcardMainMenuSelection();
                        break;
                    case "3":
                        StartStudy();
                        break;
                    case "4":
                        // View session data

                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }

        private static void StartStudy()
        {
            Console.Clear();
            int numberCorrect = 0;

            // Choose a stack
            int? stackID = UserInput.GetNumberInput("Which stack would you like to study? Enter ID:");

            List<Flashcard> flashcards = DBController.GetFlashcardsByStackId(stackID);

            // Randomise the flashcards
            Random rng = new Random();
            List<Flashcard> shuffledCards = flashcards.OrderBy(x => rng.Next()).ToList();

            foreach (Flashcard flashcard in shuffledCards)
            {
                Console.Clear();
                View.DisplaySingleFlashcard(flashcard);
                string? backTextGuess = UserInput.GetStringInput("What is the back text?");

                if (backTextGuess != null && backTextGuess.ToLower() == flashcard.BackText.ToLower())
                {
                    Console.WriteLine($"Correct! Press any key to continue.");
                    Console.ReadKey();
                    numberCorrect++;
                }
                else
                {
                    Console.WriteLine($"Incorrect. Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            // When all done, show results , on key press return to main menu
            Console.WriteLine($"\nYou got {numberCorrect}/{flashcards.Count} correct. Press any key to return to main menu.");
            Console.ReadKey();
            return;
        }

        static void HandleStackMainMenuSelection()
        {
            int? stackID =
                UserInput.GetNumberInput($"Type a stack ID to edit one, or type 0 to make a new one: \n");

            if (stackID == 0)
            {
                // Make a new stack
                string? newStackName =
                    UserInput.GetStringInput("Enter the name of the new stack:");
                DBController.AddNewStack(newStackName);

                // Allow user to add flashcards to this stack
                View.CreateFlashcardForStackMenu(newStackName);
            }
            else
            {
                // Display all flashcards in this stack
                List<Flashcard> flashcardsInStack = DBController.GetFlashcardsByStackId(stackID);
                View.DisplayFlashcardsHorizontally(flashcardsInStack);

                View.ShowStackManageMenu(stackID, flashcardsInStack);
                HandleStackManageSelection(stackID, flashcardsInStack);
            }
        }

        private static void HandleStackManageSelection(int? stackID, List<Flashcard> flashcards)
        {
            int? userStackOption = UserInput.GetNumberInput("Your choice: ");

            if (userStackOption == 1)  // Change current stack
            {
                Console.Clear();
                View.ShowStacksMainMenu(DBController.GetAllStacks());
                return;
            }
            else if (userStackOption == 2)   // Create a flashcard in stack
            {
                Console.Clear();
                string? frontText = UserInput.GetStringInput("Enter the front text for the flashcard:");
                string? backText = UserInput.GetStringInput("Enter the back text for the flashcard:");
                DBController.CreateNewFlashcard(frontText, backText, stackID);
            }
            else if (userStackOption == 3)   // Edit a flashcard in stack
            {
                int? flashcardID = UserInput.GetNumberInput("Which flashcard ID would you like to edit: ");
                string? frontText = UserInput.GetStringInput("Enter the new front text for the flashcard:");
                string? backText = UserInput.GetStringInput("Enter the new back text for the flashcard:");
                DBController.UpdateFlashcard(flashcardID, frontText, backText, stackID);
            }
            else if (userStackOption == 4)   // Delete a flashcard in stack
            {
                int? flashcardID = UserInput.GetNumberInput("Which flashcard ID would you like to delete: ");
                DBController.DeleteFlashcard(flashcardID);
            }
            else
            {
                Console.Clear();
                return;   // 0 for main menu or anything else was typed
            }
        }

        static void HandleFlashcardMainMenuSelection()
        {
            int? userSelection = UserInput.GetNumberInput("");

            if (userSelection == 1)  // Create a new flashcard
            {
                Console.Clear();
                string? frontText = UserInput.GetStringInput("Enter the front text for the flashcard:");
                string? backText = UserInput.GetStringInput("Enter the back text for the flashcard:");
                DBController.CreateNewFlashcard(frontText, backText, 1);

            }
            else if (userSelection == 2)   // Edit a flashcard
            {
                int? flashcardID = UserInput.GetNumberInput("Which flashcard ID would you like to edit: ");
                string? frontText = UserInput.GetStringInput("Enter the new front text for the flashcard:");
                string? backText = UserInput.GetStringInput("Enter the new back text for the flashcard:");
                DBController.UpdateFlashcard(flashcardID, frontText, backText, 1);
            }
            else if (userSelection == 3)   // Delete a flashcard
            {
                int? flashcardID = UserInput.GetNumberInput("Which flashcard ID would you like to delete: ");
                DBController.DeleteFlashcard(flashcardID);
            }
            else
            {
                Console.Clear();
                return;   // 0 for main menu or anything else was typed
            }
        }
    }
}