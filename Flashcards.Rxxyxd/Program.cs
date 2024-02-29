using Flashcards.Rxxyxd.Controllers;
using Flashcards.Rxxyxd.Views;

namespace Flashcards.Rxxyxd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Controller = new DatabaseController();
            var Views = new View();

            Controller.InitializeDatabase();
            Views.MainMenuView();
        }
    }
}