namespace IDisposableDemo.Model {
    public class FileWriter : IDisposable {
        private FileStream _fileStream;
        private StreamWriter _writer;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of FileWriter and opens the specified file for writing.
        /// </summary>
        /// <param name="filePath">The path of the file to open for writing.</param>
        public FileWriter(string filePath) {
            _fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            _writer = new StreamWriter(_fileStream);
        }

        /// <summary>
        /// Writes the specified text to the file.
        /// </summary>
        /// <param name="text">The text to write to the file.</param>
        public void Write(string text) {
            if (_disposed) throw new ObjectDisposedException(nameof(FileWriter));
            _writer.WriteLine(text);
        }

        /// <summary>
        /// Disposes the FileWriter, closing and releasing the underlying file resources.
        /// </summary>
        public void Dispose() {
            if (!_disposed) {
                _writer.Dispose();
                _disposed = true;
            }
        }
    }
}
