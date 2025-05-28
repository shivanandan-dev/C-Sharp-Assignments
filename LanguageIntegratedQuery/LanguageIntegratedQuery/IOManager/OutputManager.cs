using LanguageIntegratedQuery.Model;

namespace LanguageIntegratedQuery.IOManager {
    internal class OutputManager {
        /// <summary>
        /// Displays a formatted menu with a title and a list of menu actions.
        /// Each menu action is displayed with a one-based index and its description.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to display.</param>
        /// <param name="menuActions">A list of Menu objects representing the available actions.</param>
        public static void DisplayMenu(string menuTitle, List<Menu> menuActions) {
            Console.WriteLine($"============= {menuTitle} =============\n");

            for (int index = 0; index < menuActions.Count; index++) {
                Console.WriteLine($"[{index + 1}]: {menuActions[index].Description}");
            }
        }
    }
}
