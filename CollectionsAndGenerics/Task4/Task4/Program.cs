namespace Task4 {
    internal class Program {
        private static Dictionary<string, int> studentsMark = new Dictionary<string, int>() {
            { "Shivanandan", 40 },
            { "Sudharsan", 50 }
        };

        /// <summary>
        /// Displays a formatted header for the student marks table.
        /// </summary>
        /// <param name="menuTitle">The title to display above the table.</param>
        private static void DisplayMenu(string menuTitle) {
            Console.WriteLine($"\n============== {menuTitle} ==============\n");
        }

        /// <summary>
        /// Displays the student names and their marks in a table format.
        /// </summary>
        private static void DisplayDictionary() {
            Console.WriteLine("{0,-15} | {1,-5}", "Student's Name", "Marks");
            Console.WriteLine(new string('-', 23));

            foreach (var student in studentsMark) {
                Console.WriteLine("{0,-15} | {1,-5}", student.Key, student.Value);
            }
        }

        /// <summary>
        /// Entry point of the application demonstrating dictionary operations.
        /// </summary>
        public static void Main() {
            DisplayMenu("Student Marks (Before adding a student)");
            DisplayDictionary();

            studentsMark.Add("Arun", 50);
            DisplayMenu("Student Marks (After adding  a student)");
            DisplayDictionary();

            DisplayMenu("Student Marks (After Deleting a Student)");
            studentsMark.Remove("Sudharsan");
            DisplayDictionary();

            Console.ReadKey();
        }
    }
}
