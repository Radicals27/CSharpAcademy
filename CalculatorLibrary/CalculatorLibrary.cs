using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CalculatorLibrary
{
	public class Calculator
	{
		JsonWriter writer;
		string logFilePath = "calculatorlog.json";
		string runCounter = "runCounter";
		int runCount;

		public Calculator()
		{
			runCount = GetRunCount();

			StreamWriter logFile = File.CreateText(logFilePath);  // Create the log file
			logFile.AutoFlush = true;
			writer = new JsonTextWriter(logFile);  // Initialise the json writer to the logfile
			writer.Formatting = Formatting.Indented;
			writer.WriteStartObject();

			writer.WritePropertyName(runCounter);
			writer.WriteValue(runCount);

			writer.WritePropertyName("Operations");
			writer.WriteStartArray();
		}

		// Gets how many times the app has been run, stored in log file
		private int GetRunCount()
		{
			JObject logData;
			int runCount = 1;

			if (File.Exists(logFilePath))
			{
				string json = File.ReadAllText(logFilePath);
				logData = JObject.Parse(json);

				if (logData.ContainsKey(runCounter))
				{
					runCount = (int)logData[runCounter] + 1;
				}
				else
				{
					runCount = 1;
				}
			}

			return runCount;
		}

		public string GetSymbolForCalculation(string input)
		{
			switch(input)
			{
				case "a": return "+";
				case "s": return "-";
				case "m": return "x";
				case "d": return "/";
			}

			return "";
		}

		public double DoOperation(double num1, double num2, string op)
		{
			double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
			writer.WriteStartObject();
			writer.WritePropertyName("Operand1");
			writer.WriteValue(num1);
			writer.WritePropertyName("Operand2");
			writer.WriteValue(num2);
			writer.WritePropertyName("Operation");

			switch (op)
			{
				case "a":
					result = num1 + num2;
					writer.WriteValue("Add");
					break;
				case "s":
					result = num1 - num2;
					writer.WriteValue("Subtract");
					break;
				case "m":
					result = num1 * num2;
					writer.WriteValue("Multiply");
					break;
				case "d":
					// Ask the user to enter a non-zero divisor.
					if (num2 != 0)
					{
						result = num1 / num2;
					}
					writer.WriteValue("Divide");
					break;
				// Return text for an incorrect option entry.
				default:
					break;
			}
			writer.WritePropertyName("Result");
			writer.WriteValue(result);
			writer.WriteEndObject();

			return result;
		}

		public void Finish()
		{
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.Close();
		}
	}
}