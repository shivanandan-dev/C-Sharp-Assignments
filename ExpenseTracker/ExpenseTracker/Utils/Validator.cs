using System.Text.RegularExpressions;

namespace ExpenseTracker.Utils {
    internal class Validator {
        public static bool IsValidDateFormat(string dateInput) {
            string datePattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/[0-9]{4}$";
            return Regex.IsMatch(dateInput, datePattern);
        }
    }
}
