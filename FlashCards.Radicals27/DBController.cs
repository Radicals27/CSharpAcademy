using System.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;

namespace flashcard_app
{
    /// <summary>
    /// Responsible for handling all interactions with the database (DB)
    /// </summary>
    class DBController
    {
        static string? serverName = @ConfigurationManager.AppSettings.Get("ServerName");
        static string? connectionString = $"Server={serverName};Database=FlashcardApp;Trusted_Connection=True;Encrypt=False;";

        internal static void InitialiseDB()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Database connection successful.");

                // SQL command to create the 'Stack' table
                string createStackTableQuery = @"
                CREATE TABLE Stacks (
                    StackID INT IDENTITY(1,1) PRIMARY KEY,
                    StackName NVARCHAR(100) NOT NULL
                );";

                // SQL command to create the 'Flashcards' table
                string createFlashcardsTableQuery = @"
                CREATE TABLE Flashcards (
                    FlashcardID INT IDENTITY(1,1) PRIMARY KEY,
                    StackID INT NOT NULL,
                    FrontText NVARCHAR(MAX) NOT NULL,
                    BackText NVARCHAR(MAX) NOT NULL,
                    FOREIGN KEY (StackID) REFERENCES Stacks(StackID)
                );";

                // Execute the commands
                ExecuteSqlCommand(createStackTableQuery, connection, "Stack table created.");
                ExecuteSqlCommand(createFlashcardsTableQuery, connection, "Flashcards table created.");
            }
        }

        static void ExecuteSqlCommand(string sqlQuery, SqlConnection connection, string successMessage)
        {
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine(successMessage);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        internal static List<Stack> GetAllStacks()
        {
            var stacks = new List<Stack>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT StackId, StackName FROM Stacks;";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No stacks found in the database.");
                            return stacks; // Return an empty list
                        }

                        while (reader.Read())
                        {
                            var stack = new Stack
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            stacks.Add(stack);
                        }
                    }
                }
            }

            return stacks;
        }

        internal static void AddNewStack(string? stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Stacks (StackName) VALUES (@StackName);";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StackName", stackName);

                    Console.Clear();

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"New stack '{stackName}' added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add the stack. Please try again.");
                    }
                }
            }
        }

        internal static List<Flashcard> GetFlashcardsByStackId(int? stackId)
        {
            List<Flashcard> flashcards = new List<Flashcard>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT FlashcardID, FrontText, BackText FROM Flashcards WHERE StackID = @StackId;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StackID", stackId);

                    using (var reader = command.ExecuteReader())
                    {
                        Console.Clear();

                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No flashcards found in the stack.");
                            return flashcards; // Return an empty list
                        }

                        while (reader.Read())
                        {
                            var flashcard = new Flashcard
                            {
                                Id = reader.GetInt32(0),
                                FrontText = reader.GetString(1),
                                BackText = reader.GetString(2)
                            };
                            flashcards.Add(flashcard);
                        }
                    }
                }
            }

            return flashcards;
        }

        internal static void CreateNewFlashcard(string? frontText, string? backText, int? stackID)
        {
            Console.Clear();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery =
                    "INSERT INTO Flashcards (StackID, FrontText, BackText) VALUES (@StackID, @FrontText, @BackText);";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@StackID", stackID);
                    command.Parameters.AddWithValue("@FrontText", frontText);
                    command.Parameters.AddWithValue("@BackText", backText);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Flashcard created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create the flashcard.");
                    }
                }
            }
        }

        internal static void UpdateFlashcard(int? flashcardID, string? frontText, string? backText, int? stackID)
        {
            Console.Clear();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Flashcards SET FrontText = @FrontText, BackText = @BackText WHERE FlashcardID = @FlashcardID;";

                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@FlashcardID", flashcardID);
                    command.Parameters.AddWithValue("@FrontText", frontText);
                    command.Parameters.AddWithValue("@BackText", backText);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Flashcard with ID {flashcardID} updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"No flashcard found with ID {flashcardID}. Update failed.");
                    }
                }
            }
        }

        internal static void DeleteFlashcard(int? flashcardID)
        {
            Console.Clear();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Flashcards WHERE FlashcardID = @FlashcardID;";

                using (var command = new SqlCommand(deleteQuery, connection))
                {
                    // Add parameter to prevent SQL injection
                    command.Parameters.AddWithValue("@FlashcardID", flashcardID);

                    // Execute the delete query
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Flashcard with ID {flashcardID} deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"No flashcard found with ID {flashcardID}. Deletion failed.");
                    }
                }
            }
        }
    }
}