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

        internal static void ShowStacksMainMenu(List<Stack> stacks)
        {
            Console.Clear();
            var table = new Table();
            table.AddColumn("Stack Name: \n");
            int stackID = 1;

            if (stacks.Count == 0)
            {
                table.AddRow($"<No stacks found in database.>");
            }
            else
            {
                foreach (var stack in stacks)
                {
                    table.AddRow($"{stackID}. {stack.Name}");
                }
            }

            AnsiConsole.Write(table);

            Console.WriteLine("");
        }

        internal static void CreateFlashcardForStackMenu(string? stackName)
        {
            Console.Clear();
            var table = new Table();
            table.AddColumn($"Stack: {stackName}. Make a selection: \n");
            table.AddRow($"1. Create a new flashcard for this stack.");
            table.AddRow($"2. View all flashcards in this stack.");
            table.AddRow($"0. Return to main menu.");
            AnsiConsole.Write(table);
        }

        internal static void ShowFlashcardMainMenu(List<Flashcard> flashcards)
        {
            Console.Clear();
            DisplayFlashcardsHorizontally(flashcards);
            Console.WriteLine();

            var table = new Table();
            table.AddColumn($"Make a selection: \n");
            table.AddRow($"1. Create a new flashcard");
            table.AddRow($"2. Edit an existing flashcard");
            table.AddRow($"3. Delete an existing flashcard");
            table.AddRow($"0. Return to main menu.");
            AnsiConsole.Write(table);
        }

        internal static void DisplayFlashcardsHorizontally(List<Flashcard> flashcards)
        {
            if (flashcards.Count == 0 || flashcards == null)
            {
                Console.WriteLine("<No flashcards in database.>");
                return;
            }

            var grid = new Grid();

            foreach (var _ in flashcards)
            {
                grid.AddColumn(new GridColumn().Centered());
            }

            var tables = new List<Table>();

            foreach (var flashcard in flashcards)
            {
                var table = new Table()
                    .HideHeaders() // Disable headers
                    .Border(TableBorder.Rounded)
                    .AddColumn(new TableColumn("").Centered())
                    .AddColumn(new TableColumn("").Centered())
                    .AddRow("ID", flashcard.Id.ToString())
                    .AddRow("Front", flashcard.FrontText)
                    .AddRow("Back", flashcard.BackText);

                tables.Add(table);
            }

            // Add the tables horizontally in the grid
            grid.AddRow(tables.ToArray());

            AnsiConsole.Write(grid);
        }

        internal static void DisplaySingleFlashcard(Flashcard flashcard)
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("[bold yellow]Front[/]").Centered())
                .AddRow(Markup.Escape(flashcard.FrontText));

            AnsiConsole.Write(table);
        }

        internal static void ShowStackManageMenu(int? stackID, List<Flashcard> flashcards)
        {
            Console.Clear();

            View.DisplayFlashcardsHorizontally(flashcards);

            var table = new Table();
            table.AddColumn($"Current stack ID: {stackID}\n");
            table.AddRow($"1. Change current stack");
            table.AddRow($"2. Create a flashcard in stack");
            table.AddRow($"3. Edit a flashcard");
            table.AddRow($"4. Delete a flashcard");
            table.AddRow($"0. Return to main menu");

            AnsiConsole.Write(table);
        }
    }
}