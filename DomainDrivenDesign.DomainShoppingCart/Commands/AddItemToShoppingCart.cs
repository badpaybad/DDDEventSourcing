using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.DomainShoppingCart.Commands
{
    public class AddItemToShoppingCart : ICommand
    {
        public string ShoppingCartId { get; }
        public string ItemId { get; }
        public string ItemName { get; }
        public int Quanity { get; }

        public AddItemToShoppingCart(string shoppingCartId, string itemId, string itemName, int quanity)
        {
            ShoppingCartId = shoppingCartId;
            ItemId = itemId;
            ItemName = itemName;
            Quanity = quanity;
        }
    }
}