namespace Task2 {
    internal class Program {
        public static void Main() {
            // Use of var
            var name = 20;
            // name = "Shivanandan D N";
            // ^ Error: Cannot implicitly convert type 'int' to 'string'

            // Use of dynamic
            Console.WriteLine("===== Initial value and type =====");
            dynamic value = 20;
            Console.WriteLine($"Value: {value}, Type: {value.GetType()}");

            Console.WriteLine("\n===== Dynamically changed value and type =====");
            value = "Shivanandan D N";
            Console.WriteLine($"Value: {value}, Type: {value.GetType()}");

            Console.ReadKey();
        }
    }
}
