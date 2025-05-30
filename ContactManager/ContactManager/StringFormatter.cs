using System.Text.RegularExpressions;

namespace ContactManager {
    internal class StringFormatter {
        public static string AddSpaces(string input) {
            return Regex.Replace(input, "(\\B[A-Z]|\\d)", " $1");
        }
    }
}
