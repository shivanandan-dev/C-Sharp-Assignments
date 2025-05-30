namespace LanguageIntegratedQuery.Model {
    internal class Supplier {
        /// <summary>
        /// Gets or sets the unique identifier for the supplier.
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the name of the supplier.
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product associated with the supplier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Supplier"/> class.
        /// </summary>
        /// <param name="supplierId">The unique identifier for the supplier.</param>
        /// <param name="supplierName">The name of the supplier.</param>
        /// <param name="productId">The identifier of the product associated with the supplier.</param>
        public Supplier(int supplierId, string supplierName, int productId) {
            SupplierId = supplierId;
            SupplierName = supplierName;
            ProductId = productId;
        }
    }
}
