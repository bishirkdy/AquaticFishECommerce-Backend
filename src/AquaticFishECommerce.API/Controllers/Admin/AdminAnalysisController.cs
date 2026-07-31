using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Analysis;
using AquaticFishECommerce.Application.DTOs.Analysis.AnalisysPage;
using AquaticFishECommerce.Application.DTOs.Analysis.DashboardPage;
using AquaticFishECommerce.Application.DTOs.Analysis.OrderPage;
using AquaticFishECommerce.Application.DTOs.Analysis.Overall;
using AquaticFishECommerce.Application.Interfaces.Services.Analysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/analysis")]
    [Authorize(Roles = "Admin")]
    public class AnalysisController : ControllerBase
    {
        private readonly IAnalysisService _analysisService;

        public AnalysisController(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpGet("monthly-sales")]
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

        [HttpGet("monthly-products")]
        public async Task<IActionResult> GetMonthlyProducts()
        {
            var result = await _analysisService.GetMonthlyProductsAsync();

            return Ok(new ApiResponse<IEnumerable<MonthlyProductDto>>
            {
                Success = true,
                Message = "Monthly product statistics retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("category-count")]
        public async Task<IActionResult> GetCategoryCount()
        {
            var result = await _analysisService.GetCategoryCountAsync();
            return Ok(new ApiResponse<IEnumerable<CategoryCountDto>>
            {
                Success = true,
                Message = "Category statistics retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("top-dashboard")]
        public async Task<IActionResult> GetSummaryTopDashboard()
        {
            var result = await _analysisService.GetDashboardSummaryAsync();

            return Ok(new ApiResponse<DashboardSummaryDto>
            {
                Success = true,
                Message = "Dashboard summary retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("analysis-summary")]
        public async Task<IActionResult> GetSummaryOfAnalysis()
        {
            var result = await _analysisService.GetSummaryAsync();

            return Ok(new ApiResponse<AnalysisSummaryDto>
            {
                Success = true,
                Message = "Analysis summary retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("order-status-summary")]
        public async Task<IActionResult> GetOrderSummary()
        {
            var result = await _analysisService.GetOrderSummaryAsync();

            return Ok(new ApiResponse<OrderSummaryDto>
            {
                Success = true,
                Message = "Order summary retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("rating-summary")]
        public async Task<IActionResult> GetRatingSummary()
        {
            var result = await _analysisService.GetRatingSummaryAsync();

            return Ok(new ApiResponse<RatingSummaryDto>
            {
                Success = true,
                Data = result,
                Message = "Overall rating fetched successfully"
            });
        }

    }
}