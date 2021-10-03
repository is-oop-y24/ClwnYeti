namespace Shops.Classes
{
    public class ShopResponse
    {
        internal ShopResponse(bool isShopHaveProducts, Price price, int shopId)
        {
            IsShopHaveProducts = isShopHaveProducts;
            ShopId = shopId;
            Price = price;
        }

        public bool IsShopHaveProducts { get; }
        public int ShopId { get; }
        public Price Price { get; }
    }
}