using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainShoppingCart.Events
{
    public class ShoppingCartItemRemoved : IEvent
    {
        public string Id { get; }
        public string ItemId { get; }
        public int Quantity { get; }
        public DateTime UtcNow { get; }

        public ShoppingCartItemRemoved(string id, string itemId, int quantity, DateTime utcNow)
        {
            Id = id;
            ItemId = itemId;
            Quantity = quantity;
            UtcNow = utcNow;
        }

        public int Version { get; set; }
    }
}