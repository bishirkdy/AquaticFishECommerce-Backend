using AquaticFishECommerce.Application.DTOs.Payment;
using AquaticFishECommerce.Application.DTOs.Razorpay;


namespace AquaticFishECommerce.Application.Interfaces.External
{
    public interface IRazorpayService
    {
         Task<RazorpayOrderResponseDto> CreateOrderAsync(CreatePaymentDto dto);
         bool VerifyPayment(string orderId,string paymentId,string signature);
        //Task RefundAsync(string paymentId, decimal amount);

    }
}
