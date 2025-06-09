using System.Diagnostics;
using System.Text;

namespace Task2 {
    internal class Program {
        /// <summary>
        /// Asynchronously reads a file using FileStream and measures the read time.
        /// </summary>
        /// <param name="path">The full path to the input file.</param>
        /// <param name="bufferSize">The buffer size in bytes to use while reading.</param>
        /// <returns>The time taken to read the file in milliseconds.</returns>
        public static async Task<long> ReadWithFileStreamAsync(string path, int bufferSize = 8192) {
            var stopwatch = Stopwatch.StartNew();
            byte[] buffer = new byte[bufferSize];

            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, useAsync: true);
            while (await fs.ReadAsync(buffer, 0, buffer.Length) > 0) { }

            stopwatch.Stop();
            Console.WriteLine($"[Async] FileStream read time: {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Asynchronously reads a file using BufferedStream and measures the read time.
        /// </summary>
        /// <param name="path">The full path to the input file.</param>
        /// <param name="bufferSize">The buffer size in bytes to use while reading.</param>
        /// <returns>The time taken to read the file in milliseconds.</returns>
        public static async Task<long> ReadWithBufferedStreamAsync(string path, int bufferSize = 8192) {
            var stopwatch = Stopwatch.StartNew();
            byte[] buffer = new byte[bufferSize];

            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, useAsync: true);
            using BufferedStream bs = new BufferedStream(fs, bufferSize);
            while (await bs.ReadAsync(buffer, 0, buffer.Length) > 0) { }

            stopwatch.Stop();
            Console.WriteLine($"[Async] BufferedStream read time: {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Asynchronously processes a file line by line and writes the processed output to a new file.
        /// Each line is transformed to uppercase.
        /// </summary>
        /// <param name="inputPath">The full path to the input file.</param>
        /// <param name="outputPath">The full path where the processed file will be saved.</param>
        public static async Task ProcessFileLineByLineAsync(string inputPath, string outputPath) {
            var stopwatch = Stopwatch.StartNew();

            using FileStream inputFs = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
            using StreamReader reader = new StreamReader(inputFs, Encoding.UTF8);

            using FileStream outputFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
            using StreamWriter writer = new StreamWriter(outputFs, Encoding.UTF8);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null) {
                string processedLine = line.ToUpperInvariant();
                await writer.WriteLineAsync(processedLine);
            }

            await writer.FlushAsync();
            stopwatch.Stop();
            Console.WriteLine($"[Async] Processed {Path.GetFileName(inputPath)} in {stopwatch.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Processes multiple files concurrently by reading, transforming, and writing them asynchronously.
        /// </summary>
        /// <param name="inputFiles">An array of full paths to the input files.</param>
        public static async Task ProcessMultipleFilesAsync(string[] inputFiles) {
            var tasks = new List<Task>();

            foreach (var inputPath in inputFiles) {
                string outputPath = Path.Combine(Path.GetDirectoryName(inputPath)!, Path.GetFileNameWithoutExtension(inputPath) + "_async_output.txt");
                Task task = ProcessFileLineByLineAsync(inputPath, outputPath);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("[Async] All files processed concurrently.");
        }

        /// <summary>
        /// Generates a set of test files with repetitive content to simulate large data files.
        /// </summary>
        /// <param name="directory">The directory where the files will be created.</param>
        /// <param name="count">The number of files to generate.</param>
        /// <param name="sizeInMB">The size of each file in megabytes.</param>
        public static void GenerateFiles(string directory, int count, int sizeInMB) {
            Directory.CreateDirectory(directory);
            string line = "The quick brown fox jumps over the lazy dog.";

            for (int i = 1; i <= count; i++) {
                string path = Path.Combine(directory, $"input_{i}.txt");
                if (File.Exists(path)) continue;

                using FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                using StreamWriter writer = new StreamWriter(fs);
                int repeat = 1024 * 1024 / line.Length;

                for (int mb = 0; mb < sizeInMB; mb++) {
                    for (int j = 0; j < repeat; j++) {
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine($"Generated file: {path}");
            }
        }

        /// <summary>
        /// Entry point of the application. Generates test files, compares read speeds,
        /// and processes all files concurrently.
        /// </summary>
        public static async Task Main() {
            string directory = Path.Combine(Directory.GetCurrentDirectory(), "AsyncFiles");
            GenerateFiles(directory, 3, 256);

            string[] inputFiles = Directory.GetFiles(directory, "input_*.txt");

            Console.WriteLine("\n--- Asynchronous File Read Times ---");
            foreach (var file in inputFiles) {
                await ReadWithBufferedStreamAsync(file);
                await ReadWithFileStreamAsync(file);
            }

            Console.WriteLine("\n--- Concurrent Async Processing ---");
            await ProcessMultipleFilesAsync(inputFiles);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
