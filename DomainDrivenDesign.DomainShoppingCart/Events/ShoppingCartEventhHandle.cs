using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.DomainShoppingCart.Entities;

namespace DomainDrivenDesign.DomainShoppingCart.Events
{
    public class ShoppingCartEventhHandle : IEventHandle<ShoppingCartCreated>
        , IEventHandle<ShoppingCartItemAdded>, IEventHandle<ShoppingCartItemRemoved>
        , IEventHandle<ShoppingCartAllRemoved>, IEventHandle<ShoppingCartCheckedOut>
    {

        public void Handle(ShoppingCartCreated e)
        {
            SetCache(e.ShoppingCartId, new FrontEndCart()
            {
                ShoppingCartId = e.ShoppingCartId,
                Items = new List<FrontEndCartItem>(),
                Status= Enums.ShoppingCartStatus.New
            });
        }

        public void Handle(ShoppingCartItemAdded e)
        {
            var cart = GetCache(e.ShoppingCartId);

            var existed = cart.Items.SingleOrDefault(i => i.ItemId == e.ItemId);
            if (existed == null)
            {
                cart.Items.Add(new FrontEndCartItem()
                {
                    Name=e.ItemName,
                    ItemId=e.ItemId,
                    Quantity=e.Quantity,
                    Price=e.Price
                });
            }
            else
            {
                existed.Quantity = existed.Quantity + e.Quantity;
            }
            SetCache(e.ShoppingCartId, cart);
        }

        public void Handle(ShoppingCartItemRemoved e)
        {
            var cart = GetCache(e.Id);

            var existed = cart.Items.SingleOrDefault(i => i.ItemId == e.ItemId);
            if (existed != null)
            {
                existed.Quantity = existed.Quantity - e.Quantity;
                existed.Quantity = existed.Quantity < 0 ? 0 : existed.Quantity;
            }

            SetCache(e.Id, cart);

        }

        public void Handle(ShoppingCartAllRemoved e)
        {
            var cart = GetCache(e.Id);
            cart.Items.Clear();
            SetCache(e.Id, cart);
        }

        public void Handle(ShoppingCartCheckedOut e)
        {
            var cart = GetCache(e.Id);
            cart.Status= Enums.ShoppingCartStatus.Checkout;
            SetCache(e.Id, cart);
        }


        void SetCache(string cartId, FrontEndCart cart)
        {
            HttpRuntime.Cache[cartId] = cart;
        }

        FrontEndCart GetCache(string cartId)
        {
            var obj = HttpRuntime.Cache[cartId] as FrontEndCart;
            return obj;
        }
    }
}
