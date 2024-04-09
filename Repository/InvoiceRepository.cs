using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;
using System.Net.WebSockets;

namespace StoreManagement.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            if(invoice != null)
            {
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();
                return invoice;
            }
            return null;
            
        }

        public async Task<List<Invoice>> GetAllPaidAsync(int idStore, bool incluDeleted = false)
        {
            var list = await _context.Invoices.Where(obj => obj.Order.Table.StoreID == idStore && obj.Status ==true && obj.IsDeleted == incluDeleted).ToListAsync();
            return list;
        }



        public async Task<List<Invoice>> GetAllUnpaidAsync(int idStore, bool incluDeleted = false)
        {
            var list = await _context.Invoices.Where(obj=> obj.Order.Table.StoreID == idStore && obj.Status == false && obj.IsDeleted == incluDeleted).ToListAsync();
            return list;
        }

        public async Task<Invoice> GetInvoiceById(int id, bool incluDeleted = false)
        {
            var invoice = await _context.Invoices.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefaultAsync();
            return invoice;
        }

        public async Task<Invoice> GetInvoicePaidById(int idStore, int idTable, bool incluDeleted = false)
        {
            if (idStore > 0 && idTable > 0)
            {
                var invoice = await _context.Invoices.FirstOrDefaultAsync(obj => obj.Order.TableId == idTable && obj.Order.Table.StoreID == idStore && obj.IsDeleted == incluDeleted);
                return invoice;
            }
            return null;
        }

        public async Task<Invoice> GetInvoicePaidById(int id, bool incluDeleted = false)
        {
            if(id > 0)
            {
                var invoice = await _context.Invoices.Include(obj =>obj.Order).ThenInclude(obj=> obj.Table).ThenInclude(obj =>obj.Store).FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == incluDeleted && obj.Status == true);
                return invoice;
            }
            return null;
        }

        public async Task<Invoice> GetInvoiceUnpaidById(int idStore, int idTable, bool incluDeleted = false)
        {
            if(idStore > 0 && idTable >0)
            {
                var invoice = await _context.Invoices.FirstOrDefaultAsync(obj => obj.Order.TableId == idTable && obj.Order.Table.StoreID == idStore && obj.IsDeleted == incluDeleted && obj.Status == false);
                return invoice;
            }
            return null;
        }

        public async Task<Invoice> UpdateAsync(int id, bool incluDeleted = false)
        {
            var invoiceUpdate = await _context.Invoices.FirstOrDefaultAsync(obj => obj.Id == id && obj.IsDeleted == incluDeleted && obj.Status == false);
            if (invoiceUpdate != null)
            {
                invoiceUpdate.Status = true;
                await _context.SaveChangesAsync();
                return invoiceUpdate;
            }
            return null;
        }
    }
}
