using AquaticFishECommerce.Application.DTOs.Analysis.DashboardPage;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly AppDbContext _context;

        public AnalysisRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<RatingSummaryDto> GetRatingSummaryAsync()
        {
            return await _context.Reviews
                .GroupBy(r => 1)
                .Select(g => new RatingSummaryDto
                {
                    FiveStar = g.Count(r => r.Rating == 5),
                    FourStar = g.Count(r => r.Rating == 4),
                    ThreeStar = g.Count(r => r.Rating == 3),
                    TwoStar = g.Count(r => r.Rating == 2),
                    OneStar = g.Count(r => r.Rating == 1)
                })
                .FirstOrDefaultAsync() ?? new RatingSummaryDto();
        }
    }
}
