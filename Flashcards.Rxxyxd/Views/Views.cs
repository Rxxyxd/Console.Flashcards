using Spectre.Console;
using Flashcards.Rxxyxd.Controller;
using Flashcards.Rxxyxd.Models;

namespace Flashcards.Rxxyxd.Views
{
    internal class Views
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

                    case "Create Stack":
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

        internal void ManageFlashcardView()
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

        internal void StudyHistoryView()
        {

        }

        // Stack Management Methods

        internal void ViewStacks()
        {

        }

        internal void CreateStacks()
        {

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
