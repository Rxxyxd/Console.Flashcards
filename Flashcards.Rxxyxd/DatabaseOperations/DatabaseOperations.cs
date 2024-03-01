using System.Data.SqlClient;
using System.Configuration;
using Flashcards.Rxxyxd.Models;

namespace Flashcards.Rxxyxd.Controllers
{
    public class DatabaseOperations
    {
        public string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        public void InitializeDatabase()
        {
            string query;

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Stacks table
                    query = "IF OBJECT_ID(N'Stacks', N'U') IS NULL CREATE TABLE Stacks ( ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name varchar(20) UNIQUE );";
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                    // Flashcard table - Needs to be adjusted
                    query = "IF OBJECT_ID(N'Flashcards', N'U') IS NULL CREATE TABLE Flashcards ( ID INT NOT NULL FOREIGN KEY REFERENCES Stacks(ID), Question text, Answer text );";
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
                catch (SqlException) { throw; }
                catch (Exception) { throw; }
            }

        }


        // CRUD for Stack table
        public void CreateStack(Stacks newStack)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();

                        string query = "INSERT INTO Stacks (Name) VALUES (@name)";
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@name", newStack.Name);
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
                        conn.Close();
                    }
                    catch(SqlException) { throw; }
                    catch(Exception) { throw; }

                    finally
                    {
                        // Ensure Connection Closes
                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        public List<Stacks> GetStacks()
        {
            List<Stacks> stacks = new List<Stacks>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        conn.Open();

                        string query = "SELECT * FROM Stacks";
                        cmd.CommandText = query;
                        cmd.Connection = conn;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var newStack = new Stacks();
                                newStack.Name = reader["Name"].ToString();

                                // Check for DBNull before converting
                                if (reader["ID"] != DBNull.Value)
                                    newStack.ID = Convert.ToInt32(reader["ID"]);

                                stacks.Add(newStack);
                            }

                            reader.Close();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }
                    catch (SqlException) { throw; }
                    catch (Exception) { throw; }
                    
                    finally
                    {
                        // Ensure connection closes
                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
            }

            return stacks;
        }

        // CRUD Flashcards
        
    }
}
