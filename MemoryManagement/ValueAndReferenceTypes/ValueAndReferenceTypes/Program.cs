using ValueAndReferenceTypes.Model;

namespace ValueAndReferenceTypes {
    internal class Program {
        /// <summary>
        /// Demonstrates how value and reference types behave when passed as parameters.
        /// </summary>
        public static void HandleValueUpdate(int a, Person person) {
            a = 20;
            person.Name = "Shivanandan D N";
            person.Age = 24;
        }

        /// <summary>
        /// Allocates a large array of integers (reference type).
        /// The array itself is stored on the heap.
        /// </summary>
        public static void AllocateLargeArray() {
            int[] largeArray = new int[10_000_000];

            largeArray[0] = 42;
            Console.WriteLine("Large array allocated on heap. First element: " + largeArray[0]);
        }

        /// <summary>
        /// Declares a large number of local int variables (value types).
        /// These variables are stored on the stack.
        /// </summary>
        public static void ManyLocalVariablesCalculation() {
            int a1 = 1, a2 = 2, a3 = 3, a4 = 4, a5 = 5, a6 = 6, a7 = 7, a8 = 8, a9 = 9, a10 = 10;
            int a11 = 11, a12 = 12, a13 = 13, a14 = 14, a15 = 15, a16 = 16, a17 = 17, a18 = 18, a19 = 19, a20 = 20;

            int sum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9 + a10 + a11 + a12 + a13 + a14 + a15 + a16 + a17 + a18 + a19 + a20;
            Console.WriteLine("Sum of many local variables (on stack): " + sum);
        }

        public static void Main() {
            int a = 10;
            Person person = new Person {
                Name = "Shivanandan",
                Age = 23
            };
            Console.WriteLine("===== Before Passing variables as parameters =====");
            Console.WriteLine($"a: {a}\nPerson.Name: {person.Name}\nPerson.Age: {person.Age}\n");
            HandleValueUpdate(a, person);
            Console.WriteLine("===== After Passing variables as parameters =====");
            Console.WriteLine($"a: {a}\nPerson.Name: {person.Name}\nPerson.Age: {person.Age}");

            Console.WriteLine("\n===== Reference type (large array) allocation =====");
            AllocateLargeArray();

            Console.WriteLine("\n===== Value types (many local variables) calculation =====");
            ManyLocalVariablesCalculation();
            Console.ReadKey();
        }
    }
}