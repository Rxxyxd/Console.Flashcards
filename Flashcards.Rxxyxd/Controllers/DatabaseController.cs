using System.Data.SqlClient;
using System.Configuration;
using Flashcards.Rxxyxd.Models;

namespace Flashcards.Rxxyxd.Controllers
{
    public class DatabaseController
    {
        public string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        public void InitializeDatabase()
        {
            string query;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Stacks table
                query = "IF OBJECT_ID(N'Stacks', N'U') IS NULL CREATE TABLE Stacks ( ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name varchar(20) UNIQUE );";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }

                // Flashcard table
                query = "IF OBJECT_ID(N'Flashcards', N'U') IS NULL CREATE TABLE Flashcards ( ID INT NOT NULL FOREIGN KEY REFERENCES Stacks(ID), Question text, Answer text );";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }


        // CRUD Stack
        public void CreateStack(Stacks newStack)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    string query = "INSERT INTO Stacks (Name) VALUES (@name)";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", newStack.Name);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // CRUD Flashcards
        
    }
}
