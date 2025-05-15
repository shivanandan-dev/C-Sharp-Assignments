namespace ExceptionHandling.IOManager {
    public class OutputManager {
        /// <summary>
        /// Displays a menu with a title and a list of options.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to be displayed.</param>
        /// <param name="menuOptions">The list of menu options to be displayed.</param>
        public static void DisplayMenu(string menuTitle, List<string> menuOptions) {
            Console.WriteLine($"============= {menuTitle} =============\n");
            for (int index = 0; index < menuOptions.Count; index++) {
                Console.WriteLine($"[{index + 1}]: {menuOptions[index]}");
            }
        }

        /// <summary>
        /// Displays the elements of an integer array.
        /// </summary>
        /// <param name="numbers">The array of integers to display.</param>
        public static void DisplayArray(int[] numbers) {
            Console.Write("Array elements:");
            foreach (int number in numbers) {
                Console.Write($"{number} ");
            }
        }
    }
}