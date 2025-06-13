namespace ShapesApplication {
    internal class OutputManager {
        /// <summary>
        /// Displays a menu of options to the user.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to display.</param>
        /// <param name="menuActions">
        /// A dictionary where the key represents the menu option number, 
        /// and the value is a tuple containing the description of the option 
        /// and the associated action to perform.
        /// </param>
        public static void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"\n{menuTitle}");
            foreach (var menuAction in menuActions) {
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
            }
            Console.Write("\n[Menu] Enter your choice: ");
        }
    }
}
