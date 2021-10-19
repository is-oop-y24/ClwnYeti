namespace Shops.Classes
{
    public class ShopResponse
    {
        internal ShopResponse(bool isShopHaveProducts, uint price, int shopId)
        {
            IsShopHaveProducts = isShopHaveProducts;
            ShopId = shopId;
            Price = price;
        }

        public bool IsShopHaveProducts { get; }
        public int ShopId { get; }
        public uint Price { get; }
    }
}