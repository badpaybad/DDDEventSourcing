using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.DomainShoppingCart.Commands
{
    public class CheckoutShoppingCart : ICommand
    {
        public string ShoppingCartId { get; }

        public CheckoutShoppingCart(string shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }
    }
}