using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Analysis;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Application.Interfaces.Services.Analysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/analysis")]
    public class AnalysisController : ControllerBase
    {
        private readonly IAnalysisService _analysisService;

        public AnalysisController(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpGet("monthly-sales")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMonthlySales()
        {
            var result = await _analysisService.GetMonthlySalesAsync();

            return Ok(new ApiResponse<IEnumerable<MonthlySalesDto>>
            {
                Success = true,
                Message = "Monthly sales retrieved successfully.",
                Data = result
            });
        }
    }
}