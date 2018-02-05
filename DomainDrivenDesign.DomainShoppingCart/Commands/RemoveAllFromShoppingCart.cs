using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.DomainShoppingCart.Commands
{
    public class RemoveAllFromShoppingCart : ICommand
    {
        public string ShoppingCartId { get; }

        public RemoveAllFromShoppingCart(string shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }
    }
}