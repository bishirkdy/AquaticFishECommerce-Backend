using AquaticFishECommerce.Application.DTOs.Analysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services.Analysis
{
    public interface IAnalysisService
    {
        Task<IEnumerable<MonthlySalesDto>> GetMonthlySalesAsync();

    }
}
