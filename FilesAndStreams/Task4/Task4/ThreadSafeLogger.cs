using System.Text;

namespace Task4 {
    /// <summary>
    /// Subtask 2 + 3: Improved single‐file logger
    ///  - Removes the MemoryStream
    ///  - Adds a static lock to serialize all writes to “log.txt” safely
    /// </summary>
    public static class ThreadSafeLogger {
        private static readonly object _globalLock = new object();
        private static readonly string _logFilePath = "log.txt";

        /// <summary>
        /// Logs an error message into a single, shared file (“log.txt”), in a thread‐safe manner.
        /// </summary>
        public static void LogError(string errorMessage) {
            byte[] bytes = Encoding.UTF8.GetBytes(errorMessage + Environment.NewLine);

            lock (_globalLock) {
                using (var fs = new FileStream(
                           _logFilePath,
                           FileMode.Append,
                           FileAccess.Write,
                           FileShare.Read)) {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
