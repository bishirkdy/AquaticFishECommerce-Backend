using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.User
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly IValidator<LoginDto> _loginValidator;
        //private readonly IValidator<UpdateUserDto> _updateUserValidator;
        public UserController(IUserService userService)
        {
            _userService = userService;
            //_loginValidator = loginValidator;
            //_updateUserValidator = updateUserValidator;
        }

        //Controller for Update User taken by id and update
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
        {
            //var validator = await _updateUserValidator.ValidateAsync(dto);
            //if (!validator.IsValid)
            //{
            //    return BadRequest(validator.Errors);
            //}

            await _userService.UpdateAsync(id, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User updated successfully.",
            });
        }

        //Controller for Delete User taken by id and delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User deleted successfully.",
            });
        }
    }
}
