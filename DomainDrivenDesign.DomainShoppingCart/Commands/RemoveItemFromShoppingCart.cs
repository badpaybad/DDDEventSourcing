using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.DomainShoppingCart.Commands
{
    public class RemoveItemFromShoppingCart : ICommand
    {
        public RemoveItemFromShoppingCart(string shoppingCartId, string itemId, int quanity)
        {
            Quanity = quanity;
            ShoppingCartId = shoppingCartId;
            ItemId = itemId;
        }

        public string ShoppingCartId { get; }
        public string ItemId { get; }
        public int Quanity { get; }
    }
}