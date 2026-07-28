using AquaticFishECommerce.Application.DTOs.Payment;
using AquaticFishECommerce.Application.DTOs.Razorpay;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.External
{
    public interface IRazorpayService
    {
         Task<RazorpayOrderResponseDto> CreateOrderAsync(CreatePaymentDto dto);
         bool VerifyPayment(string orderId,string paymentId,string signature);

    }
}
