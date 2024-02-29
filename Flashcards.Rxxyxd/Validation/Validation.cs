namespace Flashcards.Rxxyxd.Validation
{
    public class Validate
    {
        public static bool UserInput(string userInput)
        {
            if (userInput == null || userInput == "")
            {
                return false;
            }
            else
                return true;
        }
    }
}
