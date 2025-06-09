using System.Collections.Concurrent;
using System.Text;

namespace Task4 {
    /// <summary>
    /// Subtask 4: Per‐User logger
    ///  - Each user’s errors go into a separate file (“logs/user_{userId}.txt”)
    ///  - Maintains a lock per file so that only concurrent writes to the same user file block each other
    /// </summary>
    public static class PerUserLogger {
        private static readonly string _baseLogFolder = "logs";

        private static readonly ConcurrentDictionary<string, object> _fileLocks = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Logs an error message into a file unique to the given userId.
        /// Thread‐safe per file, allowing different users to write in parallel.
        /// </summary>
        public static void LogError_ForUser(string userId, string errorMessage) {
            Directory.CreateDirectory(_baseLogFolder);
            string filePath = Path.Combine(_baseLogFolder, $"user_{userId}.txt");
            object fileLock = _fileLocks.GetOrAdd(filePath, _ => new object());

            byte[] bytes = Encoding.UTF8.GetBytes(errorMessage + Environment.NewLine);

            lock (fileLock) {
                using (var fs = new FileStream(
                           filePath,
                           FileMode.Append,
                           FileAccess.Write,
                           FileShare.Read)) {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
