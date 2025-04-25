namespace EmployeeBonusApplication {
    public class Developer : Employee {
        public Developer(string name, decimal salary) : base(name, salary) { }

        /// <summary>
        /// Calculates and returns the developer's bonus (10% of salary).
        /// </summary>
        /// <returns>The calculated bonus amount.</returns>
        public override decimal CalculateBonus() => Salary * 0.10M;

        /// <summary>
        /// Prints the details of the developer including name, position, salary, and bonus.
        /// </summary>
        public override void PrintDetails() {
            Console.WriteLine($"\nName: {Name}");
            Console.WriteLine("Position: Developer");
            Console.WriteLine($"Salary: {Salary}");
            Console.WriteLine($"Bonus: {CalculateBonus()}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
