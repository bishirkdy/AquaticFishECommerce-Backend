using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] 
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            await _userService.RegisterAsync(dto);

            return Ok(new
            {
                Message = "User registered successfully."
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);

            return Ok(new
            {
                Message = result
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
        {
            await _userService.UpdateAsync(id, dto);

            return Ok(new
            {
                Message = "User updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);

            return Ok(new
            {
                Message = "User deleted successfully."
            });
        }
    }
}
