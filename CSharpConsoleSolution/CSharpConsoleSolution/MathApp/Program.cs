namespace MathApp {
    public class Program {
        public static void Main() {
            DisplayApp.Program.Main();

            Console.WriteLine("\n=== MathApp starting ===\n");
            var ops = new MathOperations();
            Console.WriteLine($"2 + 3 = {ops.Add(2, 3)}");
            Console.WriteLine($"5 * 7 = {ops.Multiply(5, 7)}");
        }
    }
}