namespace Flashcards.Rxxyxd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Controller = new Controller.Controller();
            var Views = new Views.Views();

            Controller.InitializeDatabase();
            Views.MainMenuView();
        }
    }
}