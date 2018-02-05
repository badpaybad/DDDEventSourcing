using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainShoppingCart.Events
{
    public class ShoppingCartCreated : IEvent
    {
        public string ShoppingCartId { get; }
        public DateTime UtcNow { get; }

        public ShoppingCartCreated(string shoppingCartId, DateTime utcNow)
        {
            ShoppingCartId = shoppingCartId;
            UtcNow = utcNow;
        }

        public int Version { get; set; }
    
    }
}