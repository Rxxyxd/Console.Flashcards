using Spectre.Console;
using Flashcards.Rxxyxd.Models;

namespace Flashcards.Rxxyxd.Views
{
    internal class View
    {
         internal void MainMenuView()
        {
            bool exit = false;
            do
            {
                AnsiConsole.Clear();

                var title = new Rule("[green] Flashcards [/]");
                AnsiConsole.Write(title);
                
                var menuOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Main Menu")
                    .PageSize(10)
                    .AddChoices(new[] {
                    "Study", "Manage Stacks", "Manage Flashcards",
                    "Study Session Data", "Exit",
                    }));
                
                switch (menuOption)
                {
                    case "Start Study Session":
                        StartStudyView();
                        break;

                    case "Manage Stacks":
                        ManageStackView();
                        break;

                    case "Manage Flashcards":
                        ManageFlashcardView();
                        break;

                    case "Study Session History":
                        StudyHistoryView();
                        break;

                    case "Exit":
                        exit = true;
                        break;
                
                }
            } while (exit == false);
        }

        // Menu Methods

        internal void StartStudyView()
        {

        }

        internal void ManageStackView()
        {
            try
            {
                bool exit = false;
                do
                {

                    AnsiConsole.Clear();

                    var title = new Rule("[green] Manage Stacks [/]");
                    AnsiConsole.Write(title);

                    var option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Manage Stacks Menu")
                            .PageSize(10)
                            .AddChoices(new[]
                            {
                        "View Stacks", "Create Stacks",
                        "Update Stacks", "Delete Stacks",
                        "Back to Main Menu",
                            }));

                    switch (option)
                    {
                        case "View Stacks":
                            ViewStacks();
                            break;

                        case "Create Stacks":
                            CreateStacks();
                            break;

                        case "Update Stacks":
                            UpdateStacks();
                            break;

                        case "Delete Stacks":
                            DeleteStacks();
                            break;

                        case "Back to Main Menu":
                            exit = true;
                            break;
                    }
                } while (exit == false);
            }
            catch { throw; }
            
        }

        internal void ManageFlashcardView()
        {
            try
            {
                bool exit = false;
                do
                {
                    AnsiConsole.Clear();

                    var title = new Rule("[green] Manage Flashcards [/]");
                    AnsiConsole.Write(title);
                    var option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Manage Stacks Menu")
                            .PageSize(10)
                            .AddChoices(new[]
                            {
                        "View Flashcards", "Create Flashcards",
                        "Update Flashcards", "Delete Flashcards",
                        "Back to Main Menu",
                            }));

                    switch (option)
                    {
                        case "View Flashcards":
                            ViewFlashcards();
                            break;

                        case "Create Flashcards":
                            CreateFlashcards();
                            break;

                        case "Update Flashcards":
                            UpdateFlashcards();
                            break;

                        case "Delete Flashcards":
                            DeleteFlashcards();
                            break;

                        case "Back to Main Menu":
                            exit = true;
                            break;
                    }
                } while (exit == false);
            }
            catch (Exception) { throw; }
        }

        internal void StudyHistoryView()
        {
            
        }

        // Stack Management Methods

        internal void ViewStacks()
        {
            var Database = new Database.Database();
            var title = new Rule("View Stacks");
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");

            try
            {
                var Stacks = Database.GetStacks();
                foreach (var stack in Stacks)
                {
                    table.AddRow(stack.ID.ToString(), stack.Name.ToString());
                }
            }
            catch { throw; }
            AnsiConsole.Write(title);
            AnsiConsole.Write(table);
            Console.ReadKey();
        }

        internal void CreateStacks()
        {

            string stackName;
            var db = new Database.Database();

            AnsiConsole.Clear();
            var title = new Rule("[green]Create Stacks[/]");
            AnsiConsole.Write(title);
            var userInput = AnsiConsole.Prompt(new TextPrompt<string>($"Enter new Stack Name (or 'q' to return to menu): ")
            .PromptStyle("grey")
            .ValidationErrorMessage("Name must be less than 20 characters.")
            .Validate(input =>
            {
                if (input.Length > 20)
                {
                    return false;
                }
                return true;
            }));

            if (userInput.ToLower() != "q")
            {
                stackName = userInput.Trim();
                Stacks newStack = new();
                newStack.Name = stackName;
                try
                {
                    db.CreateStack(newStack);
                }
                catch { throw; }

                AnsiConsole.Write("[green]Stack created.[/]");
            }
        }

        internal void UpdateStacks()
        {
            int stackID;

            AnsiConsole.Clear();
            var title = new Rule("[green]Update Stacks[/]");
            AnsiConsole.Write(title);

            var db = new Database.Database();
            var updatedStack = new Stacks();
            int[] StackIds = db.GetStackIds();
            
            var userInput = AnsiConsole.Prompt(new TextPrompt<string>("Enter Stack ID (or 'q' to return to menu): ")
                .PromptStyle("grey")
                .ValidationErrorMessage("Please enter an existing stack ID")
                .Validate(input =>
                {
                    foreach (int id in StackIds)
                    {
                        if (int.TryParse(input, out int result) && result == id)
                        {
                            return true;
                        }
                    }
                    if (input.ToLower() == "q")
                    {
                        return true;
                    }
                    return false;
                }));
            if (userInput.ToLower() != "q")
            {
                stackID = int.Parse(userInput);

                userInput = AnsiConsole.Prompt(new TextPrompt<string>($"Enter new name for Stack ID '{stackID}': ")
                    .PromptStyle("grey")
                    .ValidationErrorMessage("Name must be less than 20 characters.")
                    .Validate(input =>
                    {
                        if (input.Length > 20)
                        {
                            return false;
                        }
                        return true;
                    }));

                updatedStack.Name = userInput;
                updatedStack.ID = stackID;
                try
                {
                    db.UpdateStack(updatedStack);
                }
                catch { throw; }
            }
            

        }

        internal void DeleteStacks()
        {
            int stackID;

            AnsiConsole.Clear();
            var title = new Rule("[green]Delete Stacks[/]");
            AnsiConsole.Write(title);

            var db = new Database.Database();
            var deleteStack = new Stacks();
            int[] stackIds = db.GetStackIds();

            var userInput = AnsiConsole.Prompt(new TextPrompt<string>("Enter Stack ID (or enter 'q' to return to menu): ")
                .PromptStyle("grey")
                .ValidationErrorMessage("Please enter an existing stack ID")
                .Validate(input =>
                {
                    foreach (int id in stackIds)
                    if (int.TryParse(input, out int result) && result == id)
                    {
                        return true;
                    }
                    if (input == "q")
                    {
                        return true;
                    }
                    return false;
                }));

            if (userInput != "q")
            {
                stackID = int.Parse(userInput);
                deleteStack.ID = stackID;

                try
                {
                    db.DeleteStack(deleteStack);
                }
                catch { throw; }
            }
        }

        // Flashcard Management Methods

        internal void ViewFlashcards()
        {
            try
            {
                var db = new Database.Database();
                List<Models.Flashcards> flashcards = db.GetFlashcards();

                var title = new Rule("View Flashcards");
                var table = new Table();
                table.AddColumn("ID");
                table.AddColumn("Question");
                table.AddColumn("Answer");

                foreach (Models.Flashcards flashcard in flashcards)
                {
                    if (flashcard.Question != null && flashcard.Answer != null)
                        table.AddRow(flashcard.ID.ToString(), flashcard.Question, flashcard.Answer);
                }
                AnsiConsole.Write(table);

                string[] Stacks = db.GetStackArray();


            }
            catch { throw; }
        }

        internal void CreateFlashcards()
        {
            try
            {
                AnsiConsole.Clear();
                var db = new Database.Database();
                string[] Stacks = db.GetStackArray();
                var flashcard = new Models.Flashcards();
                var title = new Rule("[green] Create Flashcard [/]");
                AnsiConsole.Write(title);

                if (Stacks.Length > 0)
                {
                    var option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Manage Stacks Menu")
                            .PageSize(10)
                            .AddChoices(Stacks));

                    flashcard.ID = db.GetStackIdByName(option);

                    flashcard.Question = AnsiConsole.Ask<string>("Enter Question: ");
                    flashcard.Answer = AnsiConsole.Ask<string>("Enter Answer: ");
                    db.CreateFlashcard(flashcard);
                    AnsiConsole.Write("Flashcard created successfully. Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    AnsiConsole.Write("There are no stacks to add flashcard to.\nCreate a stack and try again.\nPress any key to continue.");
                    Console.ReadKey();
                }
            }
            catch { throw; }
        }

        internal void UpdateFlashcards()
        {

        }

        internal void DeleteFlashcards()
        {

        }

    }
}
