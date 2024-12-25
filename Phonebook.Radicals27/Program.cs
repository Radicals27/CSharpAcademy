using Spectre.Console;

namespace Phonebook
{
    internal class Program
    {
		enum MenuOptions
		{
			AddEntry,
			DeleteEntry,
			UpdateEntry,
			ViewEntry,
			ViewAllEntries,
			Quit,
		}

        static void Main(string[] args)
        {
			var appIsRunning = true;

			while (appIsRunning)
			{
				var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
					.Title("What would you like to do?")
					.AddChoices(
						MenuOptions.AddEntry,
						MenuOptions.DeleteEntry,
						MenuOptions.UpdateEntry,
						MenuOptions.ViewEntry,
						MenuOptions.ViewAllEntries,
						MenuOptions.Quit
					)
				);

				switch (option)
				{
					case MenuOptions.AddEntry:
						EntryController.AddEntry();
						break;
					case MenuOptions.DeleteEntry:
						EntryController.DeleteEntry();
						break;
					case MenuOptions.UpdateEntry:
						EntryController.UpdateEntry();
						break;
					case MenuOptions.ViewEntry:
						EntryController.GetEntryByID();
						break;
					case MenuOptions.ViewAllEntries:
						EntryController.GetEntries();
						break;
					case MenuOptions.Quit:
						appIsRunning = false;
						Console.WriteLine("Quitting...");
						break;
					default:
						break;
				}
			}
		}
    }
}