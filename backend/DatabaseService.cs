using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace FullstackProject.Backend
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string databasePath = "app.db")
        {
            _connectionString = $"Data Source={databasePath}";
        }

        /// <summary>
        /// Initialize the database by running the init.sql script
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();

                // Read and execute init.sql
                string sql = System.IO.File.ReadAllText("../sql/init.sql");
                using var command = connection.CreateCommand();
                command.CommandText = sql;
                await command.ExecuteNonQueryAsync();

                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                throw;
            }
        }
    }
}

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}

public class LeaderboardEntry
{
    public string Username { get; set; } = string.Empty;
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
    public string GameVersion { get; set; } = string.Empty;
}

