using Spectre.Console;
using Flashcards.Rxxyxd.Controllers;
using Flashcards.Rxxyxd.Models;
using Flashcards.Rxxyxd.Validation;
using System.Data.SqlClient;

namespace Flashcards.Rxxyxd.Views
{
    internal class View
    {
        public void MainMenuView()
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
            catch (Exception) { throw; }
            
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
            var Controller = new DatabaseController();
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");

            try
            {
                var Stacks = Controller.GetStacks();
                foreach (var stack in Stacks)
                {
                    table.AddRow(stack.ID.ToString(), stack.Name.ToString());
                }
            }
            catch (Exception) { throw; }

            AnsiConsole.Write(table);
            Console.ReadKey();
        }

        internal void CreateStacks()
        {
            bool exit = false;
            bool isValid;
            var Controller = new DatabaseController();
            do
            {
                Console.Clear();
                var title = new Rule("Create Stacks");
                AnsiConsole.Write(title);
                var stackName = AnsiConsole.Ask<string>("Enter new [green]stack name[/]: ");
                isValid = Validate.UserInput(stackName);
                if (isValid)
                {
                    stackName = stackName.Trim();
                    Stacks newStack = new();
                    newStack.Name = stackName;
                    try
                    {
                        Controller.CreateStack(newStack);
                    }
                    catch (Exception) { throw; }

                    AnsiConsole.Write("[green]Stack created.[/]");
                    
                    if (AnsiConsole.Confirm("Return to Menu?"))
                    {
                        exit = true;
                    }

                }
                else
                {
                    AnsiConsole.Write("[red] Invalid Input!");
                    Thread.Sleep(3000);
                }

            } while (exit == false);
        }

        internal void UpdateStacks()
        {

        }

        internal void DeleteStacks()
        {

        }

        // Flashcard Management Methods

        internal void ViewFlashcards()
        {

        }

        internal void CreateFlashcards()
        {

        }

        internal void UpdateFlashcards()
        {

        }

        internal void DeleteFlashcards()
        {

        }

    }
}
