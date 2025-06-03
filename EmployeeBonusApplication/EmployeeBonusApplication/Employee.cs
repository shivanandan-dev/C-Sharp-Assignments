namespace EmployeeBonusApplication {
    public abstract class Employee {
        public static List<Employee> Employees = new List<Employee>();

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets the role or position of the employee.
        /// </summary>
        public abstract string Designation { get; }

        public void CreateEmployee() {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Salary: ");
            decimal salary;
            while (!decimal.TryParse(Console.ReadLine(), out salary) || salary < 0) {
                Console.Write("[Error] Invalid salary. \nEnter again: ");
            }

            Name = name;
            Salary = salary;

            Employees.Add(this);

            Console.WriteLine("[Success] Employee added.");
        }

        /// <summary>
        /// Displays all employee details from the list.
        /// </summary>
        public static void DisplayEmployee() {
            Console.Clear();
            if (Employees.Count == 0) {
                Console.WriteLine("\n[Error] No employee data.");
                return;
            }

            Console.WriteLine("===== Employee Details =====");
            foreach (Employee employee in Employees) {
                employee.PrintDetails();
            }
        }

        /// <summary>
        /// Calculates and returns the bonus amount.
        /// </summary>
        /// <returns>Bonus as a decimal value.</returns>
        public abstract decimal CalculateBonus();

        /// <summary>
        /// Prints Name, Designation, Salary, and Bonus for this employee.
        /// </summary>
        public void PrintDetails() {
            Console.WriteLine($"\nName: {Name}");
            Console.WriteLine($"Position: {Designation}");
            Console.WriteLine($"Salary: {Salary}");
            Console.WriteLine($"Bonus: {CalculateBonus()}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
