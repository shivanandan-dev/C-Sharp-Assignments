using System.Text;

// Code with Issues

//using System;
//using System.IO;
//using System.Text;

//namespace Task3 {
//    class Program {
//        static void Main(string[] args) {
//            string path = "path-to-your-file";
//            string data = "This is some test data";

//            // Writing to file using MemoryStream
//            using (MemoryStream memoryStream = new MemoryStream()) {
//                byte[] buffer = Encoding.ASCII.GetBytes(data);
//                memoryStream.Write(buffer, 0, buffer.Length);

//                // Write from MemoryStream to file
//                using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
//                    byte[] writeBuffer = memoryStream.ToArray();
//                    fileStream.Write(writeBuffer, 0, writeBuffer.Length);
//                }
//            }

//            // Reading from file using FileStream
//            using (FileStream fileStream = new FileStream(path, FileMode.Open)) {
//                byte[] buffer = new byte[1024];
//                int bytesRead;

//                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
//                    // Simulate memory inefficiency
//                    for (int i = 0; i < bytesRead; i++) {
//                        Console.Write((char)buffer[i]);
//                    }
//                    Console.WriteLine();
//                }
//            }
//        }
//    }
//}

// Corrected Code

namespace Task3 {
    internal class Program {
        static void Main() {
            string currentDirectory = Directory.GetCurrentDirectory();
            string newDirectory = Path.Combine(currentDirectory, "Files");

            if (!Directory.Exists(newDirectory))
                Directory.CreateDirectory(newDirectory);

            string path = Path.Combine(newDirectory, "file.txt");

            if (!File.Exists(path))
                File.Create(path);
            string data = "This is some test data";

            byte[] writeBuffer = Encoding.UTF8.GetBytes(data);
            using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
                fileStream.Write(writeBuffer, 0, writeBuffer.Length);
            }

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                byte[] readBuffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = fileStream.Read(readBuffer, 0, readBuffer.Length)) > 0) {
                    string chunk = Encoding.UTF8.GetString(readBuffer, 0, bytesRead);
                    Console.Write(chunk);
                }
            }
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
