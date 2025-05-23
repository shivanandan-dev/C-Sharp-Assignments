﻿using LanguageIntegratedQuery.IOManager;
using LanguageIntegratedQuery.Model;

namespace LanguageIntegratedQuery.AppInteraction {
    internal class Task1 {
        /// <summary>
        /// Handles the workflow for filtering, sorting, and displaying electronics products,
        /// and calculates the average price for the filtered products.
        /// </summary>
        public static void HandleTask() {
            Console.Clear();

            //  Question:
            // Filter products under the category "Electronics" with a price greater than $500 and select only ProductName and Price. 
            var filteredProducts = ProductList.products
                .Where(product => product.Category == Category.Electronics && product.Price > 500)
                .Select(product => new { product.ProductName, product.Price });

            // Question:
            // Using the result of the previous query, sort these filtered products in descending order of price. 
            var sortedProducts = filteredProducts
                .OrderByDescending(product => product.Price);

            // Question:
            // Find the average price of these filtered products. 
            var averagePrice = filteredProducts.Any()
                ? filteredProducts.Average(product => product.Price)
                : 0.0m;

            Console.WriteLine("========== Electronics less than price 500 (Descending) ==========\n");

            foreach (var product in sortedProducts) {
                Console.WriteLine($"{product.ProductName}: ${product.Price}");
            }

            Console.WriteLine($"\nAverage Price of all electronics: ${averagePrice}");
            InputManager.PromptForContinuation();
        }
    }
}
