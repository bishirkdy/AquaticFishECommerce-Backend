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
    }
}
