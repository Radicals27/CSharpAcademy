
/*
This app allows you to track your hours-played in terms of 
computer and video games, to help you make better habits.
*/
namespace coding_tracker
{
    /// <summary>
    /// Responsible for the main app flow
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            DBController.InitialiseDB();
            UserInput.ShowMainMenu();
        }
    }
}