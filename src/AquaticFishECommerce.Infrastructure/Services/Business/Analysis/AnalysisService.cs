using AquaticFishECommerce.Application.DTOs.Analysis;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Analysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Services.Business.Analysis
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _analysisRepository;        
        public AnalysisService(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
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

    }
}
