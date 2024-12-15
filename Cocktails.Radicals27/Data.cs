using Newtonsoft.Json;

namespace cocktails
{
    class Data
    {
        internal static Dictionary<string, Func<Task>> mainMenuOptions = new Dictionary<string, Func<Task>>
        {
            { "Ordinary Drink", Program.OrdinaryDrink },
            { "Cocktail", Program.Cocktail },
            { "Milk / Float / Shake", Program.MilkFloatShake },
            { "Other / Unknown", Program.OtherUnknown },
            { "Cocoa", Program.Cocoa },
            { "Shot", Program.Shot },
            { "Coffee / Tea", Program.CoffeeTea },
            { "Homemade Liquer", Program.HomemadeLiquer },
            { "Punch / Party Drink", Program.PunchPartyDrink },
            { "Beer", Program.Beer },
            { "Softdrink / Soda", Program.Softdrink },
            { "Return to main menu.", Program.ExitProgram }
        };
    }

    public class Drinks
    {
        [JsonProperty("drinks")]
        public List<Drink> DrinkList { get; set; }
    }

    public class Drink
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
    }
}