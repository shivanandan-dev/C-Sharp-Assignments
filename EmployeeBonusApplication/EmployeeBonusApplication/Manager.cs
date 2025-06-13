namespace EmployeeBonusApplication {
    public class Manager : Employee {
        /// <summary>
        /// The manager’s role string.
        /// </summary>
        public override string Designation => "Manager";

        /// <summary>
        /// Calculates and returns the manager's bonus (20% of salary).
        /// </summary>
        /// <returns>Bonus amount.</returns>
        public override decimal CalculateBonus() => Salary * 0.20M;
    }
}
