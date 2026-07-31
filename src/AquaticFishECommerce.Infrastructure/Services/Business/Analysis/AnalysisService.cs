using AquaticFishECommerce.Application.DTOs.Analysis;
using AquaticFishECommerce.Application.DTOs.Analysis.AnalisysPage;
using AquaticFishECommerce.Application.DTOs.Analysis.DashboardPage;
using AquaticFishECommerce.Application.DTOs.Analysis.OrderPage;
using AquaticFishECommerce.Application.DTOs.Analysis.Overall;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Analysis;
using AquaticFishECommerce.Domain.Enums;


namespace AquaticFishECommerce.Infrastructure.Services.Business.Analysis
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        public AnalysisService(IAnalysisRepository analysisRepository , IProductRepository productRepository , IOrderRepository orderRepository , IUserRepository userRepository , ICategoryRepository categoryRepository , IReviewRepository reviewRepository)
        {
            _analysisRepository = analysisRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync()
        {
            var orders = await _analysisRepository.GetOrdersAsync();

            string[] months =
            {"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};

            var current = DateTime.Now;
            var result = new List<MonthlySalesDto>();

            for (int i = 6; i >= 0; i--)
            {
                var month = current.AddMonths(-i);

                var monthlyOrders = orders.Where(o =>
                    o.CreatedAt.Month == month.Month &&
                    o.CreatedAt.Year == month.Year);

                result.Add(new MonthlySalesDto
                {
                    Month = months[month.Month - 1],
                    Sales = monthlyOrders.Sum(o => o.TotalAmount),
                    Profit = monthlyOrders.Sum(o => o.Profit)
                });
            }
            return result;
        }

        public async Task<IEnumerable<MonthlyProductDto>> GetMonthlyProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            string[] months = {"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
            var current = DateTime.Now;
            var result = new List<MonthlyProductDto>();

            for (int i = 6; i >= 0; i--)
            {
                var month = current.AddMonths(-i);

                var count = products.Count(p =>
                    p.CreatedAt.Month == month.Month &&
                    p.CreatedAt.Year == month.Year);

                result.Add(new MonthlyProductDto
                {
                    Month = months[month.Month - 1],
                    ProductCount = count
                });
            }

            return result;
        }

        public async Task<IEnumerable<CategoryCountDto>> GetCategoryCountAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            var colors = new[] {"#F97316","#06B6D4","#0B1220","#e38e51","#a4de6c"};

            var result = products
                .Where(p => p.Category != null)
                .GroupBy(p => p.Category.Name)
                .Select((group, index) => new CategoryCountDto
                {
                    Name = group.Key,
                    Value = group.Count(),
                    Fill = colors[index % colors.Length]
                })
                .ToList();

            return result;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();

            return new DashboardSummaryDto
            {
                TotalRevenue = orders.Sum(x => x.TotalAmount),
                TotalOrders = orders.Count(),
                TotalCustomers = users.Count(x => x.Role != UserRole.Admin),
                TotalProducts = products.Count()
            };
        }

        public async Task<AnalysisSummaryDto> GetSummaryAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            return new AnalysisSummaryDto
            {
                TotalSales = orders.Sum(x => x.TotalAmount),
                TotalProfit = orders.Sum(x => x.Profit),
                TotalOrders = orders.Count(),
                TotalCustomers = users.Count(x => x.Role != UserRole.Admin),
                TotalProducts = products.Count(),
                TotalCategories = categories.Count()
            };
        }

        public async Task<OrderSummaryDto> GetOrderSummaryAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return new OrderSummaryDto
            {
                TotalOrders = orders.Count(),
                OrderPlaced = orders.Count(x => x.OrderStatus == OrderStatus.OrderPlaced),
                Confirmed = orders.Count(x => x.OrderStatus == OrderStatus.Confirmed),
                Packed = orders.Count(x => x.OrderStatus == OrderStatus.Packed),
                Shipping = orders.Count(x => x.OrderStatus == OrderStatus.Shipping),
                Shipped = orders.Count(x => x.OrderStatus == OrderStatus.Shipped),
                Delivered = orders.Count(x => x.OrderStatus == OrderStatus.Delivered),
                Cancelled = orders.Count(x => x.OrderStatus == OrderStatus.Cancelled)
            };
        }

        public async Task<RatingSummaryDto> GetRatingSummaryAsync()
        {
            return await _analysisRepository.GetRatingSummaryAsync();
        }

    }
}
