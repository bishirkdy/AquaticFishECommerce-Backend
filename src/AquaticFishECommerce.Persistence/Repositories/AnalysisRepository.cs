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
    }
}
