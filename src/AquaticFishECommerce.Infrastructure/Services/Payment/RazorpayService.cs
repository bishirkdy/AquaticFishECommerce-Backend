using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.Common.Helpers;
using AquaticFishECommerce.Application.DTOs.Payment;
using AquaticFishECommerce.Application.DTOs.Razorpay;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Services.Payment
{
    public class RazorpayService : IRazorpayService
    {
        private readonly RazorpaySettings _settings;
        private readonly IProductRepository _productRepository;
        public RazorpayService(IOptions<RazorpaySettings> options , IProductRepository productRepository)
        {
            _settings = options.Value;
            _productRepository = productRepository;
        }

        public async Task<RazorpayOrderResponseDto> CreateOrderAsync(CreatePaymentDto dto)
        {
            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new NotFoundException("Product not found.");

                var discountedPrice = Math.Floor(PriceCalculation.GetDiscountedPrice(
                    product.Price,
                    product.DiscountPercentage));

                totalAmount += discountedPrice * item.Quantity;
            }

            RazorpayClient client = new RazorpayClient(
                _settings.KeyId,
                _settings.KeySecret);

            Dictionary<string, object> options = new()
    {
        { "amount", (int)(Math.Floor(totalAmount) * 100) }, // Convert to paise
        { "currency", "INR" },
        { "receipt", Guid.NewGuid().ToString() }
    };

            Order order = client.Order.Create(options);

            return new RazorpayOrderResponseDto
            {
                RazorpayOrderId = order["id"].ToString()!,
                Amount = Convert.ToDecimal(order["amount"]) / 100,
                Currency = order["currency"].ToString()!,
                Key = _settings.KeyId
            };
        }

        public bool VerifyPayment(
            string razorpayOrderId,
            string razorpayPaymentId,
            string razorpaySignature)
        {
            string payload =
                $"{razorpayOrderId}|{razorpayPaymentId}";

            using HMACSHA256 hmac =
                new HMACSHA256(
                    Encoding.UTF8.GetBytes(_settings.KeySecret));

            byte[] hash =
                hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(payload));

            string generatedSignature =
                BitConverter
                    .ToString(hash)
                    .Replace("-", "")
                    .ToLower();

            return generatedSignature == razorpaySignature;
        }
    }
}
