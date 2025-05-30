namespace InventoryManager {
    public static class Validator {
        /// <summary>
        /// Validates if the given name contains only letters and spaces.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>True if the name is valid; otherwise, false.</returns>
        public static bool IsNameValid(string name) {
            return System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$");
        }

        /// <summary>
        /// Validates if the given input is a positive decimal number.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <returns>True if the input is a valid positive decimal; otherwise, false.</returns>
        public static bool IsDecimal(string input) {
            return decimal.TryParse(input, out decimal result) && result > 0;
        }

        /// <summary>
        /// Validates if the given input is a positive integer.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <returns>True if the input is a valid positive integer; otherwise, false.</returns>
        public static bool IsPositiveInteger(string input) {
            return int.TryParse(input, out int result) && result > 0;
        }
    }
}