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

        internal static void DisplayDrinkTable(List<object> prepList)
        {
            var table = new Table();
            table.AddColumn($"Drink: {prepList[0].GetType().GetProperty("Value")?.GetValue(prepList[0])?.ToString()}");

            for (var i = 0; i < prepList.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                var key = prepList[i].GetType().GetProperty("Key")?.GetValue(prepList[i])?.ToString();
                var value = prepList[i].GetType().GetProperty("Value")?.GetValue(prepList[i])?.ToString();
                table.AddRow($"{key}: {value}");
            }

            AnsiConsole.Write(table);
        }
    }
}