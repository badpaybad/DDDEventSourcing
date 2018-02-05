using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainShoppingCart.Events
{
    public class ShoppingCartItemAdded : IEvent
    {
        public string ShoppingCartId { get; }
        public string ItemId { get; }
        public string ItemName { get; }
        public int Quantity { get; }
        public double Price { get; }
        public DateTime UtcNow { get; }

        public ShoppingCartItemAdded(string shoppingCartId, string itemId, string itemName, int quantity, double price
            , DateTime utcNow)
        {
            ShoppingCartId = shoppingCartId;
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
            UtcNow = utcNow;
        }

        public int Version { get; set; }
    }
}