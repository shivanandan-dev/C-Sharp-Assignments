namespace EmployeeBonusApplication {
    internal class Program {
        /// <summary>
        /// Main entry point of the application. Handles user input and menu navigation.
        /// </summary>
        static void Main() {
            Dictionary<int, (string, Action action)> mainMenuAction = new Dictionary<int, (string, Action)>() {
                { 1, ("Add Manager", () => EmployeeManager.CreateEmployee("Manager")) },
                { 2, ("Add Developer", () => EmployeeManager.CreateEmployee("Developer")) },
                { 3, ("Display all Employees", EmployeeManager.DisplayEmployee) },
                { 4, ("Exit", () => Environment.Exit(0)) }
            };

            do {
                Console.Clear();
                EmployeeManager.DisplayMenu("Employee Bonus System", mainMenuAction);
                bool isValid = int.TryParse(Console.ReadLine(), out int choice);
                if (isValid && mainMenuAction.ContainsKey(choice)) {
                    mainMenuAction[choice].action.Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid input. Try again.");
                }

                EmployeeManager.PromptForContinuation();
            } while (true);
        }
    }
}
