﻿using LanguageIntegratedQuery.IOManager;
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
                    new Menu("Task 1", Task1.HandleTask),
                    new Menu("Task 2", Task2.HandleTask),
                    new Menu("Task 3", Task3.HandleTask),
                    new Menu("Task 4", Task4.HandleTask),
                    new Menu("Task 5", Task5.HandleTask),
                    new Menu("Exit", () => Environment.Exit(0))
                };

                OutputManager.DisplayMenu("Main Menu", mainMenu);
                int choice = InputManager.GetMenuChoice(mainMenu);
                mainMenu[choice].Handler.Invoke();
            }
        }
    }
}
