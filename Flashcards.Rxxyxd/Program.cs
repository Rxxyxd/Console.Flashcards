using Flashcards.Rxxyxd.Database;
using Flashcards.Rxxyxd.Views;

namespace Flashcards.Rxxyxd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var DatabaseOperations = new Database.Database();
            var Views = new View();
            try
            {
                DatabaseOperations.Initialize();
                Views.MainMenuView();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Views.MainMenuView();
            }
        }
    }
}