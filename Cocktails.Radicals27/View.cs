using Spectre.Console;

namespace cocktails
{
    class View
    {
        internal static void DisplayCategoriesMenu(List<Category> categories)
        {
            int index = 1;
            foreach (var category in categories)
            {
                Console.WriteLine($"{index}. {category.strCategory}");
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
        }

        internal static void DisplayDrinkTable(List<object> objects)
        {
            var table = new Table();
            table.AddColumn($"Drink:");

            foreach (object obj in objects)
            {
                table.AddRow($"{obj}");
            }

            AnsiConsole.Write(table);
        }
    }
}