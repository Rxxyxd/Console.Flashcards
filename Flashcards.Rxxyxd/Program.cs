using Flashcards.Rxxyxd.Controllers;
using Flashcards.Rxxyxd.Views;

namespace Flashcards.Rxxyxd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Controller = new DatabaseOperations();
            var Views = new View();
            try
            {
                Controller.InitializeDatabase();
                Views.MainMenuView();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
        }
    }
}