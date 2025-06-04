using ExpenseTracker.AppInteraction;

namespace ExpenseTracker {
    internal class Program {
        /// <summary>
        /// Entry point of the application
        /// </summary>
        public static void Main() {
            ExpenseManager.AddDefaultData();
            Application.HandleMainMenu();
            Console.ReadKey();
        }
    }
}
