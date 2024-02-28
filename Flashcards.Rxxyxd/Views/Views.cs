using Spectre.Console;

namespace Flashcards.Rxxyxd.Views
{
    internal class Views
    {
        public void MainMenuView()
        {
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
                    break;
            }
        }

        internal void StartStudyView()
        {

        }

        internal void ManageStackView()
        {

        }

        internal void ManageFlashcardView()
        {

        }

        internal void StudyHistoryView()
        {

        }
    }
}
