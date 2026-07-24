using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.Common.Responses;

namespace AquaticFishECommerce.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //InvokeAsync is the method - calls automatically when a request reaches custom middleware
        public async Task InvokeAsync(HttpContext context) {
            try
            {
                await _next(context);
            }
            catch(Exception exeption)
            {
                _logger.LogError(exeption, exeption.Message);
                await HandleExceptionAsync(context, exeption);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context , Exception exception)
        {
            //ErrorResponse - DTO that represents the error response
            var response = new ErrorResponse
            {
                Success = false,
                Timestamp = DateTime.UtcNow
            };


            switch (exception)
            {
                case BadRequestException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = ex.Message;
                    response.Error = new[] { ex.Message };
                    break;

                case UnauthorizedException ex:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Message = ex.Message;
                    response.Error = new[] { ex.Message };
                    break;

                case ForbiddenException ex:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    response.Message = ex.Message;
                    response.Error = new[] { ex.Message };
                    break;

                case NotFoundException ex:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = ex.Message;
                    response.Error = new[] { ex.Message };
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "An unexpected error occurred.";
                    response.Error = new[] { exception.Message };
                    break;
            }

            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
