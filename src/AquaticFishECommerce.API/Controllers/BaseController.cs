using AquaticFishECommerce.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers
{
        [ApiController]
        public abstract class BaseController : ControllerBase
        {
            protected Guid UserId
            {
                get
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (string.IsNullOrWhiteSpace(userId))
                        throw new UnauthorizedException("User is not authenticated.");

                    return Guid.Parse(userId);
                }
            }
        }
    
}
