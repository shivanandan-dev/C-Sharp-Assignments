using System.Diagnostics;

namespace Task4 {
    internal class Program {
        private const int TOTAL_USERS = 20;
        private const int MESSAGES_PER_USER = 100;

        static async Task Main(string[] args) {
            Console.WriteLine("=== Logging Load Test ===");
            Console.WriteLine($"Total users: {TOTAL_USERS}, Messages each: {MESSAGES_PER_USER}");
            Console.WriteLine();

            CleanupOldLogs();

            OriginalLogger.LogError("warmup");
            ThreadSafeLogger.LogError("warmup");
            PerUserLogger.LogError_ForUser("warmupUser", "warmup");
            Console.WriteLine("Warmup complete.\n");

            await RunTest("A) THREAD‐SAFE (no MemoryStream, global lock)",
                          useOriginal: false, useThreadSafe: true, usePerUser: false);

            await RunTest("B) PER‐USER (no MemoryStream, per‐user files)",
                          useOriginal: false, useThreadSafe: false, usePerUser: true);

            Console.WriteLine("=== All tests finished ===");
            Console.ReadKey();
        }

        private static void CleanupOldLogs() {
            if (Directory.Exists("logs"))
                Directory.Delete("logs", recursive: true);

            if (File.Exists("log.txt"))
                File.Delete("log.txt");
        }

        private static async Task RunTest(string testName,
            bool useOriginal,
            bool useThreadSafe,
            bool usePerUser) {
            Console.WriteLine($"---- {testName} ----");
            var sw = Stopwatch.StartNew();

            Task[] tasks = new Task[TOTAL_USERS];

            for (int i = 0; i < TOTAL_USERS; i++) {
                string userId = $"User{i}";
                tasks[i] = Task.Run(() => {
                    for (int j = 0; j < MESSAGES_PER_USER; j++) {
                        string msg = $"[{DateTime.UtcNow:O}] ERR from {userId} – #{j}";

                        if (useThreadSafe) {
                            ThreadSafeLogger.LogError(msg);
                        } else if (usePerUser) {
                            PerUserLogger.LogError_ForUser(userId, msg);
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);
            sw.Stop();

            Console.WriteLine($"---- {testName} completed in {sw.ElapsedMilliseconds} ms ----\n");
        }
    }
}
