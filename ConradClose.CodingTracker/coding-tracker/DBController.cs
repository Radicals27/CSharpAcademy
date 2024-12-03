using System.Configuration;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    /// <summary>
    /// Responsible for handling all interactions with the database (DB)
    /// </summary>
    class DBController
    {
        static string connectionString = @ConfigurationManager.AppSettings.Get("ConnectionString");
        static string databasePath = @ConfigurationManager.AppSettings.Get("DatabasePath");
        static string completeConnectionString = connectionString + databasePath;

        internal static void InitialiseDB()
        {
            using (var connection = new SqliteConnection(completeConnectionString))
            {
                connection.Open();

                // Uncomment this code to delete the table (for debugging)
                // var dropCmd = connection.CreateCommand();
                // dropCmd.CommandText = "DROP TABLE IF EXISTS hours_played";
                // dropCmd.ExecuteNonQuery();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS hours_played (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER,
                        Unit TEXT
                        )";

                tableCmd.ExecuteNonQuery();

                SeedDatabase(connection);

                connection.Close();
            }
        }

        internal static void SeedDatabase(SqliteConnection connection)
        {
            var random = new Random();
            var insertCmd = connection.CreateCommand();
            var numberOfRecords = 100;
            var maxQuantity = 10;

            for (int i = 0; i < numberOfRecords; i++)
            {
                string randomDate = GenerateRandomDate(random);
                int randomQuantity = random.Next(1, maxQuantity); // Random quantity between 1 and 10
                string randomUnit = GetRandomUnit(random);

                insertCmd.CommandText =
                    "INSERT INTO hours_played (Date, Quantity, Unit) VALUES (@date, @quantity, @unit)";
                insertCmd.Parameters.Clear();
                insertCmd.Parameters.AddWithValue("@date", randomDate);
                insertCmd.Parameters.AddWithValue("@quantity", randomQuantity);
                insertCmd.Parameters.AddWithValue("@unit", randomUnit);

                insertCmd.ExecuteNonQuery();
            }
        }

        internal static List<CodingSession> GetAllRecords()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(completeConnectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM hours_played ";

                List<CodingSession> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new CodingSession
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                            Quantity = reader.GetInt32(2),
                            Unit = reader.GetString(3)
                        }); ;
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");
                }

                connection.Close();

                return tableData;
            }
        }

        internal static void Insert()
        {
            string? date = UserInput.GetDateInput();

            int? quantity = UserInput.GetNumberInput("\n\nPlease insert quantity: (then we will ask you the units of measure.)\n\n");
            string? unit = UserInput.GetStringInput("\n\nPlease insert the unit of measure: \n\n");

            using (var connection = new SqliteConnection(completeConnectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                // Parameterized query
                tableCmd.CommandText =
                    "INSERT INTO hours_played (Date, Quantity, Unit) VALUES (@date, @quantity, @unit)";

                tableCmd.Parameters.AddWithValue("@date", date);
                tableCmd.Parameters.AddWithValue("@quantity", quantity);
                tableCmd.Parameters.AddWithValue("@unit", unit);

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal static void Delete()
        {
            Console.Clear();
            GetAllRecords();

            var recordId = UserInput.GetNumberInput("\n\nPlease type the Id of the record you want to delete or type 0 to go back to Main Menu\n\n");

            using (var connection = new SqliteConnection(completeConnectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                // Parameterized query
                tableCmd.CommandText = "DELETE FROM hours_played WHERE Id = @recordId";

                tableCmd.Parameters.AddWithValue("@recordId", recordId);

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist. \n\n");
                    Delete();
                }
            }

            Console.WriteLine($"\n\nRecord with Id {recordId} was deleted. \n\n");
        }

        internal static void Update()
        {
            GetAllRecords();

            var recordId = UserInput.GetNumberInput("\n\nPlease type Id of the record would like to update. Type 0 to return to main manu.\n\n");

            using (var connection = new SqliteConnection(completeConnectionString))
            {
                connection.Open();

                // Check if the record exists
                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = "SELECT EXISTS(SELECT 1 FROM hours_played WHERE Id = @recordId)";
                checkCmd.Parameters.AddWithValue("@recordId", recordId);

                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                    connection.Close();
                    Update();
                    return;
                }

                string? date = UserInput.GetDateInput();
                int? quantity = UserInput.GetNumberInput("\n\nPlease insert number of hours of games played this session:\n\n");
                string? unit = UserInput.GetStringInput("\n\nPlease insert the unit of measure: \n\n");

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "UPDATE hours_played SET Date = @date, Quantity = @quantity, Unit = @unit WHERE Id = @recordId";

                tableCmd.Parameters.AddWithValue("@date", date);
                tableCmd.Parameters.AddWithValue("@quantity", quantity);
                tableCmd.Parameters.AddWithValue("@unit", unit);
                tableCmd.Parameters.AddWithValue("@recordId", recordId);

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        /// <summary>
        /// Counts the number of times a session was performed in a given year (YY)
        /// </summary>
        internal static void GetReportForAYear()
        {
            int codingSessionCount = 0;

            Console.WriteLine($"\n\nWhich year would you like a report for? (YY format) : \n\n");

            int yearInput = UserInput.GetTwoDigitYearFromUser();

            using (var connection = new SqliteConnection(completeConnectionString))
            {
                connection.Open();

                var reportCmd = connection.CreateCommand();
                reportCmd.CommandText =
                    @"SELECT COUNT(*) 
                    FROM hours_played 
                    WHERE substr(Date, 7, 2) = @year";  // Extracts the last 2 characters of the year

                reportCmd.Parameters.AddWithValue("@year", (yearInput % 100).ToString("00"));  // Format as two digits

                codingSessionCount = Convert.ToInt32(reportCmd.ExecuteScalar());

                connection.Close();
            }

            Console.WriteLine($"The habit was performed {codingSessionCount} times in {yearInput}.");
        }

        private static string GenerateRandomDate(Random random)
        {
            DateTime startDate = DateTime.Now.AddYears(-1); // Start date: 1 year ago
            int range = (DateTime.Now - startDate).Days;
            return startDate.AddDays(random.Next(range)).ToString("dd-MM-yy");
        }

        private static string GetRandomUnit(Random random)
        {
            string[] units = { "seconds", "hours", "minutes", "sessions" };
            return units[random.Next(units.Length)];
        }
    }
}