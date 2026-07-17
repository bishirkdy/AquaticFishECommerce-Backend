using AquaticFishECommerce.Domain.Common;
using AquaticFishECommerce.Domain.Enums;


namespace AquaticFishECommerce.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<Address> Addresses { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<CartItem> CartItems { get; set; } = [];
        public ICollection<Favorite> Favorites { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
    }
}
