namespace EmployeeBonusApplication {
    internal class EmployeeManager {
        static List<Employee> employees = new List<Employee>();

        /// <summary>
        /// Displays a menu with the specified title and a list of available options.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to display.</param>
        /// <param name="menuActions">A dictionary where the key represents the menu option number, 
        /// and the value is a tuple containing the option description and the associated action.</param>
        public static void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"===== {menuTitle} =====\n");
            foreach (var menuAction in menuActions) {
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
            }
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Prompts the user to press a key to continue.
        /// </summary>
        public static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Creates a new employee object based on the given type and adds it to the list.
        /// </summary>
        /// <param name="type">The type of employee to create (Manager/Developer).</param>
        public static void CreateEmployee(string type) {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Salary: ");
            decimal salary;
            while (!decimal.TryParse(Console.ReadLine(), out salary) || salary < 0) {
                Console.Write("[Error] Invalid salary. \nEnter again: ");
            }

            if (type == "Manager")
                employees.Add(new Manager(name, salary));
            else
                employees.Add(new Developer(name, salary));

            Console.WriteLine("[Success] Employee added.");
        }

        /// <summary>
        /// Displays all employee details from the list.
        /// </summary>
        public static void DisplayEmployee() {
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
    }
}
