using LanguageIntegratedQuery.IOManager;
using LanguageIntegratedQuery.Model;

namespace LanguageIntegratedQuery.AppInteraction {
    internal class Application {
        public static void HandleMainMenu() {
            /// <summary>
            /// Handles the main menu of the application.
            /// </summary>
            while (true) {
                Console.Clear();
                var mainMenu = new List<Menu> {
                    new Menu("Task 1", Task1.HandleTask1),
                    new Menu("Task 2", Task2.HandleTask2),
                    new Menu("Exit", () => Environment.Exit(0))
                };

                OutputManager.DisplayMenu("Main Menu", mainMenu);
                int choice = InputManager.GetMenuChoice(mainMenu);
                mainMenu[choice].Handler.Invoke();
            }
        }
    }
}
