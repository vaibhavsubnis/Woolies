using System.Collections.Generic;

namespace OnlineShopping.Api.Models
{
    public class TrolleyTotalRequest
    {
        public List<Product> Products { get; set; }
        public List<Special> Specials { get; set; }
        public List<ProductQuantity> Quantities { get; set; }
    }

    public class Special
    {
        public List<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }

    public class ProductQuantity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
