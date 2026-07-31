using AquaticFishECommerce.Application.DTOs.Analysis.DashboardPage;
using AquaticFishECommerce.Domain.Entities;


namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IAnalysisRepository
    {
        Task<List<Order>> GetOrdersAsync();
        Task<RatingSummaryDto> GetRatingSummaryAsync();
    }
}
