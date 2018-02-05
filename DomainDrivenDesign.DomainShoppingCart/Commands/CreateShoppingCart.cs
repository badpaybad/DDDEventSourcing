using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.DomainShoppingCart.Commands
{
    public class CreateShoppingCart : ICommand
    {
        public string ShoppingCartId { get; }

        public CreateShoppingCart(string shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }
    }
}