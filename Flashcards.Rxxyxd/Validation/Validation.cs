using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
