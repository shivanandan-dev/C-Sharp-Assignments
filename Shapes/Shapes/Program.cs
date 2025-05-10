using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesApplication {
    internal class Program {
        /// <summary>
        /// Entry point of the program. Displays menu and handles user interaction in a loop.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args) {
            Dictionary<int, (string, Action action)> mainMenuAction = new Dictionary<int, (string, Action)>() {
                { 1, ("Create Rectangle", ShapeApplication.CreateRectangle) },
                { 2, ("Create Circle", ShapeApplication.CreateCircle) },
                { 3, ("Exit", () => Environment.Exit(0)) }
            };

            while (true) {
                Console.Clear();
                ShapeApplication.DisplayMenu("Shape Calculator", mainMenuAction);
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && mainMenuAction.ContainsKey(choice)) {
                    mainMenuAction[choice].action.Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid input!");
                }
                ShapeApplication.PromptForContinuation();
            }
        }
    }
}
