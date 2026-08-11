using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Address;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Address;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services.Business.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository , IUserRepository userRepository , IMapper mapper , IOrderRepository orderRepository)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<AddressResponseDto> AddAddressAsync(Guid userId , CreateAddressDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                throw new NotFoundException("User not fount");
            }

            var existingAddress =
            await _addressRepository.FindExistingAddressAsync(userId,dto.Email,dto.Street,dto.Post,dto.District,dto.State,dto.Pincode,dto.Landmark);

            if (existingAddress != null)
            {
                existingAddress.UpdatedAt = DateTime.UtcNow;
                await _addressRepository.UpdateAsync(existingAddress);
                return _mapper.Map<AddressResponseDto>(existingAddress);
            }

            var address = _mapper.Map<Address>(dto);
            address.UserId = userId;
            address.UpdatedAt = DateTime.UtcNow;
             await _addressRepository.AddAsync(address);
            return _mapper.Map<AddressResponseDto>(address);
        }

        public async Task DeleteAddressAsync(Guid userId, Guid addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address == null)
                throw new NotFoundException("Address not found.");

            if (address.UserId != userId)
                throw new UnauthorizedException("Unauthorized.");

            var hasOrders = await _orderRepository.HasOrdersWithAddressAsync(addressId);

            if (hasOrders)
                throw new BadRequestException("This address is used in one or more orders and cannot be deleted.");

            await _addressRepository.DeleteAsync(address);
        }

        public async Task<IEnumerable<AddressResponseDto>> GetUserAddressesAsync(Guid userId)
        {
            var addresses = await _addressRepository.GetUserAddressesAsync(userId);

            return _mapper.Map<IEnumerable<AddressResponseDto>>(addresses);
        }

        public async Task<AddressResponseDto?> GetLastUsedAddressAsync(Guid userId)
        {
            var address = await _addressRepository.GetLastUsedAddressAsync(userId);
            if (address == null)
                return null;

            return _mapper.Map<AddressResponseDto>(address);
        }
    }
}
