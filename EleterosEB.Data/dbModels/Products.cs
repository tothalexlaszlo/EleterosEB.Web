using System;
using System.Collections.Generic;

namespace EleterosEB.Data.dbModels
{
    public partial class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitInStock { get; set; }

        public virtual Categories Category { get; set; }
    }
}
