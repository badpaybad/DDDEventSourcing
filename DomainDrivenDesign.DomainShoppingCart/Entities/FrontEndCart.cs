using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenDesign.DomainShoppingCart.Entities
{
    public class FrontEndCart
    {
        public string ShoppingCartId { get; set; }
        public List<FrontEndCartItem> Items { get; set; }
        public Enums.ShoppingCartStatus Status { get; set; }
    }

    public class FrontEndCartItem
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
