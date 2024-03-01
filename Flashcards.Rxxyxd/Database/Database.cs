﻿using System.Data.SqlClient;
using System.Configuration;
using Flashcards.Rxxyxd.Models;

namespace Flashcards.Rxxyxd.Database
{
    internal class Database
    {
        public string? connectionString;
        public Database()
        {
            connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        }
        protected internal void Initialize()
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
                    query = "IF OBJECT_ID(N'Flashcards', N'U') IS NULL CREATE TABLE Flashcards ( ID INT NOT NULL FOREIGN KEY REFERENCES Stacks(ID) ON DELETE CASCADE, Question text, Answer text );";
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
        protected internal void CreateStack(Stacks newStack)
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
                    catch(SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {
                            throw new ArgumentException("Stacks.Name already exists.");
                        }
                        else
                        {
                            throw;
                        }
                    }
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

        protected internal int GetStackCount()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM Stacks;";
                        cmd.CommandText = query;
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException) { throw; }
                    catch (Exception) { throw; }

                    finally
                    {

                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        protected internal List<Stacks> GetStacks()
        {
            List<Stacks> stacks = new List<Stacks>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
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
                            conn.Dispose();
                        }
                    }
                    catch (SqlException) { throw; }
                    catch (Exception) { throw; }
                    
                    finally
                    {

                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
            }

            return stacks;
        }

        protected internal void UpdateStack(Stacks updatedStack)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Stacks SET Name = @Name WHERE ID = @ID;";
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@Name", updatedStack.Name);
                        cmd.Parameters.AddWithValue("@ID", updatedStack.ID);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                    }
                    catch (SqlException) { throw; }
                    catch (Exception) { throw; }

                    finally
                    {

                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        protected internal void DeleteStack(Stacks deleteStack)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Stacks WHERE ID = @ID;";
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@ID", deleteStack.ID);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                    }
                    catch (SqlException) { throw; }
                    catch (Exception) { throw; }

                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        protected internal string[] GetStackArray()
        {
            List<string> Stacks = new List<string>();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    
                    conn.Open();
                    var query = "SELECT * FROM Stacks;";
                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["name"] != DBNull.Value)
                            {
                                Stacks.Add(reader["name"].ToString());
                            }
                        }
                    }
                }
            }
            return Stacks.ToArray();
        }

        protected internal int GetStackIdByName(string name)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    var query = "SELECT ID FROM Stacks WHERE Name = @Name";
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue(query, name);
                    return Convert.ToInt16(cmd.ExecuteScalar());
                    
                }
            }
        }

        // CRUD Flashcards
        
        protected internal List<Models.Flashcards> GetFlashcards()
        {
            List<Models.Flashcards> flashcards = new List<Models.Flashcards>();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        var query = "SELECT * FROM Flashcards;";
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var flashcard = new Models.Flashcards();

                                if (reader["ID"] != DBNull.Value)
                                {
                                    flashcard.ID = Convert.ToInt16(reader["ID"]);
                                }

                                flashcard.Question = reader["Question"].ToString();
                                flashcard.Answer = reader["Answer"].ToString();

                                flashcards.Add(flashcard);
                            }
                            reader.Close();
                        }
                        cmd.Dispose();
                        conn.Close();
                    }
                    catch { throw; }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed)
                        { 
                            conn.Close();
                        }
                    }
                }
            }
            return flashcards;
        }

        protected internal void CreateFlashcard(Models.Flashcards flashcards)
        {

        }
    }
}
