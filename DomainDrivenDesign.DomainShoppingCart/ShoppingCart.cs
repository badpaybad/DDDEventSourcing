using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.DomainShoppingCart.Entities;
using DomainDrivenDesign.DomainShoppingCart.Events;

namespace DomainDrivenDesign.DomainShoppingCart
{
    public class ShoppingCart : AggregateRoot
    {
        public ShoppingCart()
        {
        }

        private List<KeyValuePair<string, double>> _idItems;
        private DateTime _lastUpdate;
        private Enums.ShoppingCartStatus _status;

        public override string Id { get; set; }

        #region  private Apply

        void Apply(ShoppingCartCreated e)
        {
            Id = e.ShoppingCartId;
            _idItems = new List<KeyValuePair<string, double>>();
            _lastUpdate = e.UtcNow;
            _status = Enums.ShoppingCartStatus.New;
        }

        void Apply(ShoppingCartItemAdded e)
        {
            for (int i = 0; i < e.Quantity; i++)
            {
                _idItems.Add(new KeyValuePair<string, double>(e.ItemId, e.Price));
            }
            _lastUpdate = e.UtcNow;
        }
        void Apply(ShoppingCartItemRemoved e)
        {
            var temp = _idItems.Where(i => i.Key == e.ItemId).Take(e.Quantity);

            foreach (var r in temp)
            {
                _idItems.Remove(r);
            }

            _lastUpdate = e.UtcNow;
        }
        void Apply(ShoppingCartAllRemoved e)
        {
            _idItems.Clear();

            _lastUpdate = e.UtcNow;
        }

        void Apply(ShoppingCartCheckedOut e)
        {
            _status = Enums.ShoppingCartStatus.Checkout;
            _lastUpdate = e.UtcNow;
        }


        #endregion

        public ShoppingCart(string shoppingCartId)
        {
            if (string.IsNullOrEmpty(shoppingCartId)) throw new NoNullAllowedException(nameof(shoppingCartId) + "is null or empty");

            ApplyChange(new ShoppingCartCreated(shoppingCartId, DateTime.UtcNow));
        }

        public void Add(string itemId, int quantity)
        {
            if (string.IsNullOrEmpty(itemId)) throw new NoNullAllowedException(nameof(itemId) + "is null or empty");
            if (quantity <= 0) throw new NotSupportedException(nameof(quantity) + " not allow zero or negative");
            //if(_status== Enums.ShoppingCartStatus.Checkout) throw  new NotSupportedException("Shopping cart was checked out, should create other shopping cart to buy new item");

            double price = 10;
            string itemName = "Sample";
            // u can connect to dbread to read price by itemId, or other services to read price
            // eg:
            //using (var db=new TestDbContext())
            //{
            //    price = db.Product.Where(i => i.Sku == itemId).Select(i => i.Price).SingleOrDefault();
            //    itemName = db.Product.Where(i => i.Sku == itemId).Select(i => i.Name).SingleOrDefault();
            //}
            // itemName can connect to dbread to read, or just pass through param
            //eg: void Add(string itemId, string itemName, int quantity)

            ApplyChange(new ShoppingCartItemAdded(Id, itemId, itemName, quantity, price, DateTime.UtcNow));
        }

        public void Remove(string itemId, int quantity)
        {
            if (string.IsNullOrEmpty(itemId)) throw new NoNullAllowedException(nameof(itemId) + "is null or empty");
            if (quantity <= 0) throw new NotSupportedException(nameof(quantity) + " not allow zero or negative");
            //if (_status == Enums.ShoppingCartStatus.Checkout) throw new NotSupportedException("Shopping cart was checked out, should create other shopping cart to buy new item");

            ApplyChange(new ShoppingCartItemRemoved(Id, itemId, quantity, DateTime.UtcNow));
        }

        public void RemoveAll()
        {
            // : )) this logic for sample only
            if (_status == Enums.ShoppingCartStatus.Checkout) throw new NotSupportedException("Shopping cart was checked out, can not remove all item : )) ");

            ApplyChange(new ShoppingCartAllRemoved(Id, DateTime.UtcNow));
        }

        public void Checkout()
        {
            if (_status == Enums.ShoppingCartStatus.Checkout) throw new NotSupportedException("Shopping cart already checked out");

            ApplyChange(new ShoppingCartCheckedOut(Id, DateTime.UtcNow));
        }

    }

}
