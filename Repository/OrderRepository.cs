using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            if(order != null)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(int id, bool incluDeleted = false)
        {
            try
            {
                if (id != 0)
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && o.IsDeleted == incluDeleted && o.StatusPay == false);
                    if(order != null)
                    {
                        _context.Orders.Remove(order);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Order>> GetListOrder(int idStore, bool incluDeleted = false)
        {
            try
            {
                var order = await _context.Orders.Where(obj => obj.Table.StoreID == idStore && obj.status == false && obj.IsDeleted == incluDeleted && obj.StatusPay == false).ToListAsync();
                return order.ToList();
            }catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Order>> GetListOrderAccept(int id, bool incluDeleted = false)
        {
            var order = await _context.Orders.Where(obj => obj.Table.StoreID == id && obj.status == true && obj.IsDeleted == incluDeleted && obj.StatusPay == false).ToListAsync();
            return order.ToList();
        }

        public async Task<List<OrderDetail>> GetListOrderDetailsByIdOrder(int id)
        {
            var orderDetails = await _context.OrderDetails.Where(o => o.OrderId == id ).Include(obj=>obj.FoodItem).ToListAsync();
            return orderDetails.ToList();
        }

        public async Task<Order> GetOrderPaidAsync(int idStore, int idTable, bool incluDeleted = false)
        {
            
            var order = await _context.Orders.Include("Table").FirstOrDefaultAsync(obj => obj.Table.StoreID == idStore && obj.TableId == idTable && obj.StatusPay == true);
            return order;
        }

        public async Task<Order> GetOrderAcceptedById(int id, bool incluDeleted = false)
        {
            if (id == 0)
            {
                return null;
            }
            var order = await _context.Orders.Include("Table").FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == incluDeleted );
            return order;
        }

        public async Task<Order> GetOrderById(int id, bool incluDeleted = false)
        {
            if(id == 0)
            {
                return null;
            }
            var order = await _context.Orders.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == incluDeleted && obj.status == false);
            return order;
        }

        public async Task<Order> UpdateAsync(int id, bool incluDeleted = false)
        {
            var orderUpdate = await _context.Orders.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == incluDeleted && obj.status == false);
            if (orderUpdate != null)
            {
                orderUpdate.status = true;
                await _context.SaveChangesAsync();
                return orderUpdate;
            }
            return null;
        }

        public async Task<Order> UpdatePayAsync(int id, bool incluDeleted = false)
        {
            var orderUpdate = await _context.Orders.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == incluDeleted && obj.status == true );
            if (orderUpdate != null)
            {
                orderUpdate.StatusPay = true;
                await _context.SaveChangesAsync();
                return orderUpdate;
            }
            return null;
        }

        public Task<List<Order>> GetListOrder(List<int> idStore, bool incluDeleted = false)
        {
            throw new NotImplementedException();
        }
    }
}
