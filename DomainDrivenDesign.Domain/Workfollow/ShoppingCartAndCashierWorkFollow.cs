using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.DomainShoppingCart.Events;

namespace DomainDrivenDesign.Domain.Workfollow
{
    public class ShoppingCartAndCashierWorkFollow : IEventHandle<ShoppingCartCheckedOut>
    {
        public void Handle(ShoppingCartCheckedOut e)
        {
            //call any command as your business required.
            Console.WriteLine("Shopping cart checked out, so that we call cashier domain to create order and do payment");
        }
    }
}
