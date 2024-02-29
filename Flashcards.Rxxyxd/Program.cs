using Flashcards.Rxxyxd.Views;
using Flashcards.Rxxyxd.Controller;

namespace Flashcards.Rxxyxd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Controller = new Controller.Controller();
            var Views = new Views.Views(); 
            Views.MainMenuView();
        }
    }
}