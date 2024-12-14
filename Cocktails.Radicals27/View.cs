using Spectre.Console;

namespace cocktails
{
    class View
    {
        internal static void DisplayMenu(Dictionary<string, Action> options)
        {
            int index = 1;
            foreach (var option in options.Keys)
            {
                Console.WriteLine($"{index}. {option}");
                index++;
            }
        }
    }
}