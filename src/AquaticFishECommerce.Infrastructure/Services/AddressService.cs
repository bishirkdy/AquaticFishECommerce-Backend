using AquaticFishECommerce.Application.DTOs.Address;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository , IUserRepository userRepository , IMapper mapper)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AddressResponseDto> AddAddressAsync(Guid userId , CreateAddressDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                throw new Exception("User not fount");
            }
            var address = _mapper.Map<Address>(dto);
            address.UserId = userId;
             await _addressRepository.AddAsync(address);
            return _mapper.Map<AddressResponseDto>(address);
        }
    }
}
