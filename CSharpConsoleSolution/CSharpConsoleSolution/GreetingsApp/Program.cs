using MathApp;

namespace GreetingsApp {
    class Program {
        static void Main() {
            Console.WriteLine("\n=== GreetingApp starting ===\n");
            var ops = new MathOperations();
            Console.WriteLine($"10 - 4 = {ops.Subtract(10, 4)}");

            MathApp.Program.Main();

            Console.ReadKey();
        }
    }
}
