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
            var response = new ErrorResponse();
            switch (exception)
            {
                case BadRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = exception.Message;
                    break;

                case UnauthorizedException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Message = exception.Message;
                    break;

                case ForbiddenException:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    response.Message = exception.Message;
                    break;

                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    response.Message = exception.Message;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Unexpected Error Occured.";
                    break;
            }
        }
    }
}
