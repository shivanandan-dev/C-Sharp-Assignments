using ExpenseTracker.AppInteraction;

namespace ExpenseTracker {
    internal class Program {
        public static void Main() {
            TransactionManager.AddDefaultData();
            Application.HandleMainMenu();
            Console.ReadKey();
        }
    }
}
