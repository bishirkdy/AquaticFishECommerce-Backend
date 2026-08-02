namespace AquaticFishECommerce.Application.Common.Helpers
{
    public static class PriceCalculation
    {
        public static decimal GetDiscountedPrice(decimal price, decimal discountPercentage)
        {
            return price - (price * discountPercentage / 100);
        }
    }
}
