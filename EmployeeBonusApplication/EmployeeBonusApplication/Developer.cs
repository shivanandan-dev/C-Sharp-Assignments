namespace EmployeeBonusApplication {
    public class Developer : Employee {
        /// <summary>
        /// The developer’s role string.
        /// </summary>
        public override string Designation => "Developer";

        /// <summary>
        /// Calculates and returns the developer's bonus (10% of salary).
        /// </summary>
        /// <returns>The calculated bonus amount.</returns>
        public override decimal CalculateBonus() => Salary * 0.10M;
    }
}
