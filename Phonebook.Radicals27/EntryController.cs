
using Spectre.Console;

namespace PhoneBook
{
	internal class EntryController
	{
		internal static void AddEntry()
		{
			var name = AnsiConsole.Ask<string>("Entry name: ");
			var phoneNumber = AnsiConsole.Ask<string>("Their phone number: ");
			using var db = new EntryContext();
			db.Add(new PhoneBookEntry { Name = name , PhoneNumber = phoneNumber });
			db.SaveChanges();
		}

		internal static void DeleteEntry()
		{

		}

		internal static void GetEntryByID()
		{

		}

		internal static void UpdateEntry()
		{

		}

		internal static void GetEntries()
		{

		}
	}
}