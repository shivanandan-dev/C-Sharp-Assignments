using System.Text.RegularExpressions;

namespace InventoryManager {
    internal class StringFormatter {
        /// <summary>
        /// Adds spaces before each uppercase letter (except at the start) and before each digit in the input string.
        /// </summary>
        /// <param name="input">The original string to process.</param>
        /// <returns>A new string with spaces inserted before uppercase letters and digits.</returns>
        public static string AddSpaces(string input) {
            return Regex.Replace(input, "(\\B[A-Z]|\\d)", " $1");
        }
    }
}
