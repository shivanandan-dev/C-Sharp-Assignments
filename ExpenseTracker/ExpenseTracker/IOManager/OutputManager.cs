namespace ExpenseTracker.IOManager {
    internal class OutputManager {
        public static void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"===== {menuTitle} =====\n");
            foreach (var menuAction in menuActions)
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
            Console.Write($"\n[Menu] Enter choice: ");
        }

        public static void DisplayInvalidChoiceError() {
            Console.WriteLine("[Error] Invalid choice.");
            Console.Write($"[Menu] Enter choice: ");
        }

        public static void DisplayNegativeAmountError() {
            Console.WriteLine("[Error] Amount should be a positive decimal.");
            Console.Write($"Enter Amount: ");
        }

        public static void DisplayInvalidInputError() {
            Console.WriteLine("[Error] Invalid input.");
        }

        public static void DisplaySuccessMessage(string message) {
            Console.WriteLine($"[Success] {message}");
        }
    }
}
