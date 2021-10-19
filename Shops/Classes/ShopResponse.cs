namespace Shops.Classes
{
    public class ShopResponse
    {
        internal ShopResponse(bool isShopHaveProducts, Money price, int shopId)
        {
            IsShopHaveProducts = isShopHaveProducts;
            ShopId = shopId;
            Price = price;
        }

        public bool IsShopHaveProducts { get; }
        public int ShopId { get; }
        public Money Price { get; }
    }
}