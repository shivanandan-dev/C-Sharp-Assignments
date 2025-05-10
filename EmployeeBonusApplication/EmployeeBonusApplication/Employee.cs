namespace EmployeeBonusApplication {
    public abstract class Employee {
        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="name">The name of the employee.</param>
        /// <param name="salary">The salary of the employee.</param>
        public Employee(string name, decimal salary) {
            Name = name;
            Salary = salary;
        }

        /// <summary>
        /// Calculates and returns the bonus amount.
        /// </summary>
        /// <returns>Bonus as a decimal value.</returns>
        public abstract decimal CalculateBonus();

        /// <summary>
        /// Prints the details of the employee.
        /// </summary>
        public abstract void PrintDetails();
    }
}
