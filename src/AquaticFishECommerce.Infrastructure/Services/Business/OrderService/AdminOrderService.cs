using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Order;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services.Business.OrderServices
{
    public class AdminOrderService : IAdminOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public AdminOrderService(IOrderRepository orderRepository , IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        //Service for get all order for admin
        public async Task<List<OrderResponseDto>> GetAllOrderAsync()
        {
            var orders = await _orderRepository.GetAllOrderAsync();
            var res = new List<OrderResponseDto>();
            foreach (var order in orders)
            {
                var orderDto = _mapper.Map<OrderResponseDto>(order);
                res.Add(orderDto);
            }
            return res;
        }
    }
}
