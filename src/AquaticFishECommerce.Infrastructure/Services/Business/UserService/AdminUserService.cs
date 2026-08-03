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
        public AdminUserService(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
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

            await _userRepository.DeleteAsync(user);
        }
    }
}
