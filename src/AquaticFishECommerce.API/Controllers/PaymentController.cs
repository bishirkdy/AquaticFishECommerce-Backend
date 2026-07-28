using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Payment;
using AquaticFishECommerce.Application.DTOs.Razorpay;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IRazorpayService _razorpayService;
        private readonly IOrderService _orderService;

        public PaymentController(
            IRazorpayService razorpayService,
            IOrderService orderService)
        {
            _razorpayService = razorpayService;
            _orderService = orderService;
        }

        /// Create Razorpay Order
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(CreatePaymentDto dto)
        {
            var response = await _razorpayService.CreateOrderAsync(dto);

            return Ok(new ApiResponse<RazorpayOrderResponseDto>
            {
                Success = true,
                Message = "Amount added to razorpay",
                Data = response
            }
                );
        }

        /// Verify Payment & Create Order
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyPayment(VerifyPaymentDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool verified = _razorpayService.VerifyPayment(
                dto.RazorpayOrderId,
                dto.RazorpayPaymentId,
                dto.RazorpaySignature);

            if (!verified)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Payment verification failed."
                });
            }

            // Payment verified
            var order = await _orderService.CreateOrderAsync(Guid.Parse(userId) , dto.Order);

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Message = "Payment Successful",
                Data = order
            });
        }
    }
}