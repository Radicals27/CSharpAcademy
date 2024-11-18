using CalculatorLibrary;
using System.Text.RegularExpressions;
using static CalculatorLibrary.Calculator;

namespace CalculatorProgram
{
	class Program
	{
		static private List<CalculationData> calculationHistory = new List<CalculationData>();
		static Calculator calculator = new Calculator();
		static bool endApp = false;

		static void Main(string[] args)
		{
			Console.CancelKeyPress += OnExit;
			AppDomain.CurrentDomain.ProcessExit += OnExit;

			while (!endApp)
			{
				ShowMainMenu();
			}

			calculator.Finish();
			return;
		}

		private static void ShowMainMenu()
		{
			Console.WriteLine("Console Calculator in C#\r");
			Console.WriteLine("------------------------\n");
			Console.WriteLine("Welcome to the calculator, choose an option:");
			Console.WriteLine("\t1. Do a calculation");
			Console.WriteLine("\t2. Show operation history");
			Console.WriteLine("\t3. Delete operation history");
			Console.WriteLine("\t4. Quit");
			Console.Write("Your option? ");

			string? menuChoice = Console.ReadLine();

			if (menuChoice.Length == 1 && char.IsDigit(menuChoice[0]))
			{
				int userInput = int.Parse(menuChoice);

				switch (userInput)
				{
					case 1:
						DoCalculation();
						break;
					case 2:
						ShowHistory();
						break;
					case 3:
						DeleteHistory();
						break;
					case 4:
						endApp = true;
						break;
				}
			}
			else
			{
				Console.Write("Invalid input, try again.");
				Console.ReadLine();
			}
		}

		private static void ShowHistory()
		{
			// Go back through the latest 9 calculations
			for (int i = calculationHistory.Count - 1; i >= 0; i--)
			{
				Console.WriteLine($"{i}. " +
					$"{calculationHistory[i].num1} " +
					$"{calculationHistory[i].operand} " +
					$"{calculationHistory[i].num2} = " +
					$" {calculationHistory[i].finalResult} "
					);

				if (i <= calculationHistory.Count - 10)
				{
					break;
				}
			}

			Console.WriteLine("Which calculation would you like to access?  Enter a number...");
			string calculationSelection = Console.ReadLine();
			HandleHistorySelection(calculationSelection);
		}

		private static void HandleHistorySelection(string calculationSelection)
		{
			Console.Clear();

			if (calculationSelection.Length == 1 && char.IsDigit(calculationSelection[0]))
			{
				int userInput = int.Parse(calculationSelection);


			}
		}

		private static void DeleteHistory()
		{
			calculationHistory.Clear();
		}

		static void DoCalculation()
		{
			string? numInput1 = "";
			string? numInput2 = "";
			double result = 0;

			Console.Write("Type a number, and then press Enter: ");
			numInput1 = Console.ReadLine();

			double cleanNum1 = 0;
			while (!double.TryParse(numInput1, out cleanNum1))
			{
				Console.Write("This is not valid input. Please enter an integer value: ");
				numInput1 = Console.ReadLine();
			}

			Console.Write("Type another number, and then press Enter: ");
			numInput2 = Console.ReadLine();

			double cleanNum2 = 0;
			while (!double.TryParse(numInput2, out cleanNum2))
			{
				Console.Write("This is not valid input. Please enter an integer value: ");
				numInput2 = Console.ReadLine();
			}

			Console.WriteLine("Choose an operator from the following list:");
			Console.WriteLine("\ta - Add");
			Console.WriteLine("\ts - Subtract");
			Console.WriteLine("\tm - Multiply");
			Console.WriteLine("\td - Divide");
			Console.Write("Your option? ");

			string? op = Console.ReadLine();

			// Validate input is not null, and matches the pattern
			if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
			{
				Console.WriteLine("Error: Unrecognized input.");
			}
			else
			{
				try
				{
					result = calculator.DoOperation(cleanNum1, cleanNum2, op);
					CalculationData calcData = new CalculationData(cleanNum1, cleanNum2, calculator.GetSymbolForCalculation(op), result);
					calculationHistory.Add(calcData);

					if (double.IsNaN(result))
					{
						Console.WriteLine("This operation will result in a mathematical error.\n");
					}
					else Console.WriteLine("Your result: {0:0.##}\n", result);
				}
				catch (Exception e)
				{
					Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
				}
			}
			Console.WriteLine("Press any key to return to the main menu.\n");

			Console.ReadLine();
			Console.Clear();
		}

		static void OnExit(object sender, EventArgs e)
		{
			calculator.Finish();
		}
	}
}