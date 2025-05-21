using ValueAndReferenceTypes.Model;

namespace ValueAndReferenceTypes {
    internal class Program {
        /// <summary>
        /// Demonstrates how value and reference types behave when passed as parameters.
        /// Modifies the value type 'a' and updates the reference type 'person'.
        /// </summary>
        /// <param name="a">A value type parameter (int)</param>
        /// <param name="person">A reference type parameter (Person object)</param>
        public static void HandleValueUpdate(int a, Person person) {
            a = 20;
            person.Name = "Shivanandan D N";
            person.Age = 24;
        }

        /// <summary>
        /// Entry point demonstrating difference between value and reference types when passed to a method.
        /// </summary>
        public static void Main() {
            int a = 10;
            Person person = new Person {
                Name = "Shivanandan",
                Age = 23
            };

            Console.WriteLine("===== Before passing variables as parameters =====");
            Console.WriteLine($"a: {a}\nPerson.Name: {person.Name}\nPerson.Age: {person.Age}\n");
            
            HandleValueUpdate(a, person);
            
            Console.WriteLine("===== After passing variables as parameters =====");
            Console.WriteLine($"a: {a}\nPerson.Name: {person.Name}\nPerson.Age: {person.Age}");
            
            Console.ReadKey();
        }
    }
}
