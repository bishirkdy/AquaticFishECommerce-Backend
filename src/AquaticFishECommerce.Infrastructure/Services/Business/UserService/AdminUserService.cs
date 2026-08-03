using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.User;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services.Business.User
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        public AdminUserService(IUserRepository userRepository , IMapper mapper , IOrderRepository orderRepository , IFavoriteRepository favoriteRepository , ICartItemRepository cartItemRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }
        // Get all users
        public async Task<IEnumerable<UserListDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsyncUser();
            return _mapper.Map<IEnumerable<UserListDto>>(users);
        }

        //block and unblock
        public async Task<bool> UpdateUserBlockStatusAsync(Guid userId, bool isBlocked)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            user.IsBlocked = isBlocked;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        //Delete user
        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            bool hasOrders = await _orderRepository.HasOrdersByUserIdAsync(userId);
            if (hasOrders)
                throw new BadRequestException("Cannot delete a user who has placed orders.");

            // Delete cart items and favorite of user
            await _cartItemRepository.DeleteByUserIdAsync(userId);
            await _favoriteRepository.DeleteByUserIdAsync(userId);

            // Delete user
            await _userRepository.DeleteAsync(user);

            await _userRepository.DeleteAsync(user);
        }
    }
}
