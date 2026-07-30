namespace AquaticFishECommerce.Application.DTOs.Analysis.OrderPage
{
    public class OrderSummaryDto
    {
        public int TotalOrders { get; set; }
        public int OrderPlaced { get; set; }
        public int Confirmed { get; set; }
        public int Packed { get; set; }
        public int Shipping { get; set; }
        public int Shipped { get; set; }
        public int Delivered { get; set; }
        public int Cancelled { get; set; }
    }
}
