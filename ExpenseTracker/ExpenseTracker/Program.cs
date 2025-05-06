using ExpenseTracker.AppInteraction;

namespace ExpenseTracker {
    internal class Program {
        public static void Main() {
            ExpenseManager.AddDefaultData();
            Application.HandleMainMenu();
            Console.ReadKey();
        }
    }
}
