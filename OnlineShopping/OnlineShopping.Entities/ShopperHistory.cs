using System.Collections.Generic;

namespace OnlineShopping.Entities
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
