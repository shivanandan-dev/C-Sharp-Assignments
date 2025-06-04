using ExpenseTracker.AppInteraction;

namespace ExpenseTracker {
    internal class Program {
        /// <summary>
        /// Entry point of the application
        /// </summary>
        public static void Main() {
            TransactionManager.AddDefaultData();
            Application.HandleMainMenu();
            Console.ReadKey();
        }
    }
}
