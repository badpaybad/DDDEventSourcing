using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Commands;
using DomainDrivenDesign.Core.EventSourcingRepository;
using DomainDrivenDesign.Core.Implements;

namespace DomainDrivenDesign.DomainShoppingCart.Commands
{
    public class ShoppingCartCommandHandle : ICommandHandle<CreateShoppingCart>
          , ICommandHandle<AddItemToShoppingCart>, ICommandHandle<RemoveItemFromShoppingCart>
          , ICommandHandle<RemoveAllFromShoppingCart>, ICommandHandle<CheckoutShoppingCart>
    {
        ICqrsEventSourcingRepository<ShoppingCart>
            _repository = new CqrsEventSourcingRepository<ShoppingCart>(new EventPublisher());

        public void Handle(CreateShoppingCart c)
        {
            _repository.CreateNew(new ShoppingCart(c.ShoppingCartId));
        }

        public void Handle(AddItemToShoppingCart c)
        {
            var obj = _repository.Get(c.ShoppingCartId);
            obj.Add(c.ItemId, c.Quanity);
            _repository.Save(obj);
        }

        public void Handle(RemoveItemFromShoppingCart c)
        {
            _repository.GetDoSave(c.ShoppingCartId, (d) =>
            {
                d.Remove(c.ItemId, c.Quanity);
            });
        }


        public void Handle(RemoveAllFromShoppingCart c)
        {
            _repository.GetDoSave(c.ShoppingCartId, (d) =>
            {
                d.RemoveAll();
            });
        }

        public void Handle(CheckoutShoppingCart c)
        {
            _repository.GetDoSave(c.ShoppingCartId, (d) =>
            {
                d.Checkout();
            });
        }
    }
}
