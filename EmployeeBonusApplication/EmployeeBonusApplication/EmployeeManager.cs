namespace EmployeeBonusApplication {
    internal class EmployeeManager {
        static List<Employee> employees = new List<Employee>();

        /// <summary>
        /// Displays a menu with the specified title and a list of available options.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to display.</param>
        /// <param name="menuActions">A dictionary where the key represents the menu option number, 
        /// and the value is a tuple containing the option description and the associated action.</param>
        public static void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"===== {menuTitle} =====\n");
            foreach (var menuAction in menuActions) {
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
            }
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Prompts the user to press a key to continue.
        /// </summary>
        public static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
