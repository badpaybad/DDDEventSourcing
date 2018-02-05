using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainShoppingCart.Events
{
    public class ShoppingCartAllRemoved : IEvent
    {
        public string Id { get; }
        public DateTime UtcNow { get; }

        public ShoppingCartAllRemoved(string id, DateTime utcNow)
        {
            Id = id;
            UtcNow = utcNow;
        }

        public int Version { get; set; }
    }
}