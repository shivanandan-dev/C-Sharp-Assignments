namespace ExceptionHandling.IOManager {
    public class OutputManager {
        /// <summary>
        /// Displays a menu with a title and a list of options.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to be displayed.</param>
        /// <param name="menuOptions">The list of menu options to be displayed.</param>
        public static void DisplayMenu(string menuTitle, List<string> menuOptions) {
            int indexValue = 1;
            Console.WriteLine($"============= {menuTitle} =============\n");
            foreach (string option in menuOptions) {
                Console.WriteLine($"[{indexValue++}]: {option}");
            }
        }

        /// <summary>
        /// Prompts the user to press any key to continue execution.
        /// </summary>
        public static void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}