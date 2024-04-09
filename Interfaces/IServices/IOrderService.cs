using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.Interfaces.IServices
{
    public interface IOrderService
    {
        public Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
        public Task<List<OrderDTO>> GetListOrder(int idStore);
        public Task DeleteAsync(int id, bool incluDeleted = false);
        public Task<OrderDTO> UpdateAsync(int id , bool incluDeleted = false);
        public Task<OrderDTO> UpdatePayAsync(int id, bool incluDeleted = false);
        public Task<OrderDTO> GetOrderById(int id, bool incluDeleted = false);
        public Task<List<OrderDetailsDTO>> GetListOrderDetailsByIdOrder(int id);
        public Task<OrderDTO> GetOrderAcceptedById(int id, bool incluDeleted = false);
        public Task<List<OrderDTO>> GetListOrderAccept(int id, bool incluDeleted = false);
        public Task<Order> GetOrderPaidAsync(int idStorte, int idTale, bool incluDeleted = false);
    }
}
