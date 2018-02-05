using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainShoppingCart.Events
{
    public class ShoppingCartCheckedOut : IEvent
    {
        public string Id { get; }
        public DateTime UtcNow { get; }

        public ShoppingCartCheckedOut(string id, DateTime utcNow)
        {
            Id = id;
            UtcNow = utcNow;
        }

        public int Version { get; set; }
    }
}