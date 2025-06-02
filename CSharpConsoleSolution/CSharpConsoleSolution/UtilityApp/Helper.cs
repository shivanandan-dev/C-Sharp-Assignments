namespace UtilityApp {
    public static class Helper {
        /// <summary>
        /// Formats a timestamp for display.
        /// </summary>
        public static string FormatTimestamp(DateTime dt) {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Wraps a message with separators.
        /// </summary>
        public static string Beautify(string msg) {
            return $"***  {msg}  ***";
        }
    }
}
