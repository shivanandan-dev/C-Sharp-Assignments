namespace EmployeeBonusApplication {
    internal class BonusApplication {
        static List<Employee> employees = new List<Employee>();

        /// <summary>
        /// Displays the main menu with available options.
        /// </summary>
        static void DisplayMainMenu() {
            Console.WriteLine("===== Employee Bonus System =====\n");
            Console.WriteLine("1. Add Manager");
            Console.WriteLine("2. Add Developer");
            Console.WriteLine("3. Display All Employees");
            Console.WriteLine("4. Exit");
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Prompts the user to press a key to continue.
        /// </summary>
        static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Creates a new employee object based on the given type and adds it to the list.
        /// </summary>
        /// <param name="type">The type of employee to create (Manager/Developer).</param>
        static void CreateEmployee(string type) {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Salary: ");
            decimal salary;
            while (!decimal.TryParse(Console.ReadLine(), out salary) || salary < 0) {
                Console.Write("[Error] Invalid salary. \nEnter again: ");
            }

            var employeeDetail = new Manager(name, salary); // Replace with proper type switch if you add Developer

            employees.Add(employeeDetail);
            Console.WriteLine("[Success] Employee added.");
        }

        /// <summary>
        /// Displays all employee details from the list.
        /// </summary>
        static void DisplayEmployee() {
            Console.Clear();
            if (employees.Count == 0) {
                Console.WriteLine("\n[Error] No employee data.");
                return;
            }

            Console.WriteLine("===== Employee Details =====");
            foreach (Employee employee in employees) {
                employee.PrintDetails();
            }
        }

        /// <summary>
        /// Main entry point of the application. Handles user input and menu navigation.
        /// </summary>
        static void Main() {
            Dictionary<int, Action> action = new Dictionary<int, Action>() {
                { 1, () => CreateEmployee("Manager") },
                { 3, DisplayEmployee },
                { 4, () => Environment.Exit(0) }
            };

            do {
                Console.Clear();
                DisplayMainMenu();
                bool isValid = int.TryParse(Console.ReadLine(), out int choice);
                if (!isValid) {
                    Console.WriteLine("[Error] Invalid input. Try Again");
                }
                if (isValid && action.ContainsKey(choice)) {
                    action[choice]();
                }
                else {
                    Console.WriteLine("[Error] Invalid input.");
                }

                PromptForContinuation();
            } while (true);
        }
    }
}
