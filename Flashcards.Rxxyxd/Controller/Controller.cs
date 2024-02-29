using System.Data.SqlClient;

namespace Flashcards.Rxxyxd.Controller
{
    public class Controller
    {
        public string connectionString = null; // need to create app config
        public void InitializeDatabase()
        {
            string query;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Stacks table
                query = "CREATE TABLE IF NOT EXISTS Stacks ( ID INT NOT NULL, Name TEXT, PRIMARYKEY (ID) );";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }

                // Flashcard table
                query = "CREATE TABLE IF NOT EXISTS Flashcards ( ID INT NOT NULL, Question TEXT, Answer TEXT, FOREIGN KEY (ID) REFERENCES Stacks(ID) );";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }


        // CRUD Stack
        public void CreateStack(string stackName)
        {
            
        }

        // CRUD Flashcards
        
    }
}
