using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepo = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> CreateAsync(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            var result = await _orderRepo.CreateAsync(order);
            return _mapper.Map<OrderDTO>(result);
        }

        public async Task DeleteAsync(int id, bool incluDeleted = false)
        {
            await _orderRepo.DeleteAsync(id, incluDeleted);
        }

        public async Task<List<OrderDTO>> GetListOrder(int idStore)
        {
            var list = await _orderRepo.GetListOrder(idStore);
            return _mapper.Map<List<OrderDTO>>(list);
        }

        public async Task<List<OrderDTO>> GetListOrderAccept(int id, bool incluDeleted = false)
        {
            var list = await _orderRepo.GetListOrderAccept(id);
            return _mapper.Map<List<OrderDTO>>(list);
        }

        public async Task<List<OrderDetailsDTO>> GetListOrderDetailsByIdOrder(int id)
        {
            var list = await _orderRepo.GetListOrderDetailsByIdOrder(id);
            return _mapper.Map<List<OrderDetailsDTO>>(list);
        }

        public async Task<Order> GetOrderPaidAsync(int idStore, int idTable, bool incluDeleted = false)
        {
            var order = await _orderRepo.GetOrderPaidAsync(idStore, idTable);
            return _mapper.Map<Order>(order);
        }

        public async Task<OrderDTO> GetOrderAcceptedById(int id, bool incluDeleted = false)
        {
            var order = await _orderRepo.GetOrderAcceptedById(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> GetOrderById(int id, bool incluDeleted = false)
        {
            var order = await _orderRepo.GetOrderById(id);
            return _mapper.Map<OrderDTO>(order);

        }

        public async Task<OrderDTO> UpdateAsync(int id , bool incluDeleted = false)
        {
            var result = await _orderRepo.UpdateAsync(id,incluDeleted);
            return _mapper.Map<OrderDTO>(result);
        }

        public async Task<OrderDTO> UpdatePayAsync(int id, bool incluDeleted = false)
        {
            var result = await _orderRepo.UpdatePayAsync(id, incluDeleted);
            return _mapper.Map<OrderDTO>(result);
        }
    }
}
