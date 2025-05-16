using ExceptionHandling.IOManager;

namespace ExceptionHandling.AppInteraction {
    internal class Application {
        /// <summary>
        /// Handles the main menu of the application.
        /// </summary>
        public static void HandleMainMenu() {
            while (true) {
                Console.Clear();
                List<string> mainMenuOptions = new List<string>() {
                    "Division",
                    "Array out of bounds demo",
                    "Unhandled Exception",
                    "Exit"
                };

                OutputManager.DisplayMenu("Main Menu", mainMenuOptions);
                int choice = InputManager.GetMenuChoice(mainMenuOptions);

                switch (choice) {
                    case 1:
                        DivisionApplication.HandleDivision();
                        break;
                    case 2:
                        ArrayApplication.HandleArrayOutOfBoundsDemo();
                        break;
                    case 3:
                        UnhandledExceptionApplication.HandleUnhandledExceptionDemo();
                        break;
                    case 4:
                        return;
                }
            }
        }
    }
}