using Spectre.Console;

namespace flashcard_app
{
    /// <summary>
    /// Responsible for showing the UI to the user
    /// </summary>
    class View
    {
        internal static void DisplayMainMenuOptions()
        {
            var table = new Table();
            table.AddColumn("Welcome to the flashcard app, make a selection: \n");
            table.AddRow("1. Manage Stacks");
            table.AddRow("2. Manage Flashcards");
            table.AddRow("3. Study");
            table.AddRow("4. View Study Session Data");

            table.AddRow("0. Exit");

            AnsiConsole.Write(table);
        }

        internal static void ShowStacksMenu(List<Stack> stacks)
        {
            Console.Clear();
            var table = new Table();
            table.AddColumn("Stack Name: \n");
            int stackID = 1;

            if (stacks.Count == 0)
            {
                table.AddRow($"(Stacks table is currently empty.)");
            }
            else
            {
                foreach (var stack in stacks)
                {
                    table.AddRow($"{stackID}. {stack.Name}");
                }
            }

            AnsiConsole.Write(table);
        }

        internal static void CreateFlashcardForStackMenu(string stackName)
        {
            Console.Clear();
            var table = new Table();
            table.AddColumn($"Stack: {stackName}. Make a selection: \n");
            table.AddRow($"1. Create a new flashcard for this stack.");
            table.AddRow($"2. View all flashcards in this stack.");
            table.AddRow($"0. Return to main menu.");
            AnsiConsole.Write(table);
        }
    }
}