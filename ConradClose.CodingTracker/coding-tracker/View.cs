using System.Globalization;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace coding_tracker
{
    /// <summary>
    /// Responsible for showing the UI to the user
    /// </summary>
    class View
    {
        internal static void DisplayMainMenuOptions()
        {
            var table = new Table();
            table.AddColumn("Welcome to the game-tracking app Main Menu \nWhat would you like to do? \n");
            table.AddRow("1. View all records");
            table.AddRow("2. Insert a record");
            table.AddRow("3. Delete a record");
            table.AddRow("4. Update a record");
            table.AddRow("5. Get report for a year");
            table.AddRow("0. Exit");

            AnsiConsole.Write(table);
        }

        internal static void DisplayAllEntries(List<CodingSession> _tableData)
        {
            var table = new Table();
            table.AddColumn("All entries:");

            foreach (var dw in _tableData)
            {
                table.AddRow($"{dw.Id} - {dw.Date.ToString("dd-MMM-yyyy")} - S: {dw.StartTime}, E: {dw.EndTime}, Duration: {dw.Duration} minutes");
            }

            AnsiConsole.Write(table);
        }
    }
}