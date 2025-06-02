using IDisposableDemo.Model;

namespace IDisposableDemo {
    internal class Program {
        /// <summary>
        /// Entry point of the application.
        /// Writes to a file using a disposable writer and then attempts to read the file.
        /// </summary>
        public static void Main() {
            string filePath = "files//demo.txt";
            Console.WriteLine("Writing to File...");

            using (var writer = new FileWriter(filePath)) {
                writer.Write("Hello, IDisposable!");
                writer.Write("This text demostrate file release after disposal.");
            }

            Console.WriteLine("Attempting to open and read file after using block");

            try {
                using (var reader = new StreamReader(filePath)) {
                    string contents = reader.ReadToEnd();
                    Console.WriteLine("File Contents: ");
                    Console.WriteLine(contents);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
