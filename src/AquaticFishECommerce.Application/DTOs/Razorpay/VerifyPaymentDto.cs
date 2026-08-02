using AquaticFishECommerce.Application.DTOs.Order;

namespace AquaticFishECommerce.Application.DTOs.Razorpay
{
    public class VerifyPaymentDto
    {
        public string RazorpayOrderId { get; set; } = string.Empty;

        public string RazorpayPaymentId { get; set; } = string.Empty;

        public string RazorpaySignature { get; set; } = string.Empty;

        public CreateOrderDto Order { get; set; } = null!;
    }

}
