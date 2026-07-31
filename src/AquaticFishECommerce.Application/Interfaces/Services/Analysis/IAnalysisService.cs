using AquaticFishECommerce.Application.DTOs.Analysis;
using AquaticFishECommerce.Application.DTOs.Analysis.AnalisysPage;
using AquaticFishECommerce.Application.DTOs.Analysis.DashboardPage;
using AquaticFishECommerce.Application.DTOs.Analysis.OrderPage;
using AquaticFishECommerce.Application.DTOs.Analysis.Overall;


namespace AquaticFishECommerce.Application.Interfaces.Services.Analysis
{
    public interface IAnalysisService
    {
        Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync();
        Task<IEnumerable<MonthlyProductDto>> GetMonthlyProductsAsync();
        Task<IEnumerable<CategoryCountDto>> GetCategoryCountAsync();
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
        Task<AnalysisSummaryDto> GetSummaryAsync();
        Task<OrderSummaryDto> GetOrderSummaryAsync();
        Task<RatingSummaryDto> GetRatingSummaryAsync();
    }
}
