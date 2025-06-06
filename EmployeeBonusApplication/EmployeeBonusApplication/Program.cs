namespace EmployeeBonusApplication {
    internal class Program {
        /// <summary>
        /// Main entry point of the application. Handles user input and menu navigation.
        /// </summary>
        static void Main() {
            Employee manager = new Manager();
            Employee developer = new Developer();

            Dictionary<int, (string, Action action)> mainMenuAction = new Dictionary<int, (string, Action)>() {
                { 1, ("Add Manager", () => manager.CreateEmployee()) },
                { 2, ("Add Developer", () => developer.CreateEmployee()) },
                { 3, ("Display all Employees", Employee.DisplayEmployee) },
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
