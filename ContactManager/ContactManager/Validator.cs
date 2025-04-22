namespace ContactManager {
    /// <summary>
    /// Provides methods to validate user input such as names, phone numbers, and email addresses.
    /// </summary>
    internal class Validator {
        /// <summary>
        /// Validates a person's name to ensure it contains only alphabets and spaces.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>
        /// An empty string if the name is valid, otherwise an error message indicating 
        /// that the name must contain only letters and spaces.
        /// </returns>
        public bool IsNameValid(string name) {
            return System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$");
        }

        /// <summary>
        /// Validates a phone number to ensure it is either:
        /// 1. Exactly 10 digits long.
        /// 2. A valid international number with a country code prefixed by '+'.
        /// </summary>
        /// <param name="number">The phone number to validate.</param>
        /// <returns>
        /// An empty string if the phone number is valid, otherwise an error message indicating 
        /// that the phone number must meet the specified format.
        /// </returns>
        public bool IsNumberValid(string number) {
            return System.Text.RegularExpressions.Regex.IsMatch(number, @"^(\d{10}|\+\d+)$");
        }

        /// <summary>
        /// Validates an email address to ensure it conforms to a standard email format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>
        /// An empty string if the email address is valid, otherwise an error message indicating 
        /// that the email format is invalid.
        /// </returns>
        public bool IsEmailValid(string email) {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        }
    }
}