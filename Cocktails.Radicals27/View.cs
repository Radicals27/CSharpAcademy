using Spectre.Console;

namespace cocktails
{
    class View
    {
        internal static void DisplayMenu(Dictionary<string, Func<Task>> options)
        {
            int index = 1;
            foreach (var option in options.Keys)
            {
                Console.WriteLine($"{index}. {option}");
                index++;
            }
        }

        internal static void DisplayDrinksTable(List<Drink> drinks)
        {
            var table = new Table();
            table.AddColumn("Drinks:");

            foreach (Drink drink in drinks)
            {
                table.AddRow($"{drink.idDrink}. {drink.strDrink}");
            }

            AnsiConsole.Write(table);
            Console.ReadKey();
        }
    }
}