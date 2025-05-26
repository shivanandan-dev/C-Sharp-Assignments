namespace EmployeeBonusApplication {
    public class Manager : Employee {
        /// <summary>
        /// Calculates and returns the manager's bonus (20% of salary).
        /// </summary>
        /// <returns>Bonus amount.</returns>
        public override decimal CalculateBonus() => Salary * 0.20M;

        /// <summary>
        /// Prints the details of the manager.
        /// </summary>
        public override void PrintDetails() {
            Console.WriteLine($"\nName: {Name}");
            Console.WriteLine("Position: Manager");
            Console.WriteLine($"Salary: {Salary}");
            Console.WriteLine($"Bonus: {CalculateBonus()}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
