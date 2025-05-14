namespace ContactManager {
    /// <summary>
    /// Provides methods to validate user input such as names, phone numbers, and email addresses.
    /// </summary>
    internal static class Validator {
        /// <summary>
        /// Validates a person's name to ensure it contains only alphabets and spaces.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>
        /// An empty string if the name is valid, otherwise an error message indicating 
        /// that the name must contain only letters and spaces.
        /// </returns>
        public static bool IsNameValid(string name) {
            return System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$");
        }

        /// <summary>
        /// Validates a phone number to ensure it is either:
        /// 1. Exactly 10 digits long and does not start with '0'.
        /// 2. A valid international number with a country code prefixed by '+' and a 10-digit phone number:
        ///    - Country code can be 2, 3, or 4 digits long.
        ///    - The 10-digit phone number cannot start with '0'.
        /// </summary>
        /// <param name="number">The phone number to validate.</param>
        /// <returns>
        /// True if the phone number is valid, otherwise false.
        /// </returns>
        public static bool IsNumberValid(string number) {
            return System.Text.RegularExpressions.Regex.IsMatch(number, @"^([1-9]\d{9}|\+\d{2,4}[1-9]\d{9})$");
        }

        /// <summary>
        /// Validates an email address to ensure it conforms to a standard email format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>
        /// An empty string if the email address is valid, otherwise an error message indicating 
        /// that the email format is invalid.
        /// </returns>
        public static bool IsEmailValid(string email) {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        }
    }
}