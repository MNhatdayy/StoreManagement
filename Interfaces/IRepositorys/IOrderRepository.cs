using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IOrderRepository
    {
        public Task<Order> CreateAsync(Order order);
        public Task<List<Order>> GetListOrder(int idStore, bool incluDeleted = false);
        public Task<Order> UpdateAsync(int id, bool incluDeleted = false);
        public Task<Order> UpdatePayAsync(int id, bool incluDeleted = false);
        public Task<bool> DeleteAsync(int id, bool incluDeleted = false );
        public Task<Order> GetOrderById(int id,bool incluDeleted = false);
        public Task<List<OrderDetail>> GetListOrderDetailsByIdOrder(int id);
        public Task<Order> GetOrderAcceptedById(int id, bool incluDeleted = false);
        public Task<List<Order>> GetListOrderAccept(int id, bool incluDeleted = false);
        public Task<Order> GetOrderPaidAsync(int idStore, int idTable, bool incluDeleted = false);
    
    }
}
