
namespace AquaticFishECommerce.Application.DTOs.User
{
    //This is Dto for take user 
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool isBlocked { get; set; }
    }
}
