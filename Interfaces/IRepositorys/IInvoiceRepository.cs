using Microsoft.AspNetCore.Mvc;
using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IInvoiceRepository
    {
        public Task<Invoice> CreateAsync(Invoice invoice);
        public Task<List<Invoice>> GetAllUnpaidAsync(int idStore, bool incluDeleted = false);
        public Task<Invoice> GetInvoiceUnpaidById(int idStore, int idTable, bool incluDeleted = false);
        public Task<List<Invoice>> GetAllPaidAsync(int idStore, bool incluDeleted = false);
        public Task<Invoice> GetInvoicePaidById(int idStore, int idTable, bool incluDeleted = false);
        public Task<Invoice> UpdateAsync (int id , bool incluDeleted = false);
        public Task<Invoice> GetInvoicePaidById(int id, bool incluDeleted = false);
        public Task<Invoice> GetInvoiceById(int id, bool incluDeleted = false);

    }
}
