using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core;

namespace DomainDrivenDesign.DomainShoppingCart
{
    public class ShoppingCart:AggregateRoot
    {
        public ShoppingCart()
        {
        }

        public override string Id { get; set; }


    }
}
