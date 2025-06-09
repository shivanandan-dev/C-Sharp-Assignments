using System.Diagnostics;
using System.Text;

namespace Task1 {
    internal class Program {
        /// <summary>
        /// Generates a large file of specified size (in MB) with repetitive text content.
        /// </summary>
        public static void GenerateLargerFile(string path, int sizeInMB) {
            using FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            using StreamWriter writer = new StreamWriter(fs);

            string line = "The quick brown fox jumps over the lazy dog.";
            int repeat = 1024 * 1024 / line.Length;

            for (int i = 0; i < sizeInMB; i++) {
                for (int j = 0; j < repeat; j++) {
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"Generated {sizeInMB}MB file at {path}");
        }

        /// <summary>
        /// Reads a file using FileStream and returns the time taken (without loading full content).
        /// </summary>
        public static long ReadWithFileStream(string path, int bufferSize = 4096) {
            var stopwatch = Stopwatch.StartNew();
            byte[] buffer = new byte[bufferSize];

            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            while (fs.Read(buffer, 0, buffer.Length) > 0) { }

            stopwatch.Stop();
            Console.WriteLine($"FileStream read time: {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Reads a file using BufferedStream and returns the time taken (without loading full content).
        /// </summary>
        public static long ReadWithBufferedStream(string path, int bufferSize = 4096) {
            var stopwatch = Stopwatch.StartNew();
            byte[] buffer = new byte[bufferSize];

            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using BufferedStream bs = new BufferedStream(fs, bufferSize);
            while (bs.Read(buffer, 0, buffer.Length) > 0) { }

            stopwatch.Stop();
            Console.WriteLine($"BufferedStream read time: {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Processes and writes the file line by line to avoid memory overflow.
        /// </summary>
        public static void ProcessFileLineByLine(string inputPath, string outputPath) {
            var stopwatch = Stopwatch.StartNew();

            using FileStream inputFs = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader reader = new StreamReader(inputFs, Encoding.UTF8);

            using FileStream outputFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
            using StreamWriter writer = new StreamWriter(outputFs, Encoding.UTF8);

            string? line;
            while ((line = reader.ReadLine()) != null) {
                string processedLine = line.ToUpperInvariant();
                writer.WriteLine(processedLine);
            }

            stopwatch.Stop();
            Console.WriteLine($"Line-by-line processing completed in {stopwatch.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Entry point of the application.
        /// </summary>
        public static void Main() {
            string currentDirectory = Directory.GetCurrentDirectory();
            string newDirectory = Path.Combine(currentDirectory, "Files");
            Directory.CreateDirectory(newDirectory);

            string inputPath = Path.Combine(newDirectory, "large_input.txt");
            string outputPath = Path.Combine(newDirectory, "processed_output.txt");

            GenerateLargerFile(inputPath, 1024);

            Console.WriteLine("\n--- Comparing File Read Times ---");
            long fileStreamTime = ReadWithFileStream(inputPath);
            long bufferedStreamTime = ReadWithBufferedStream(inputPath);

            Console.WriteLine($"\nDifference: {fileStreamTime - bufferedStreamTime} ms");

            Console.WriteLine("\n--- Processing File ---");
            ProcessFileLineByLine(inputPath, outputPath);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
