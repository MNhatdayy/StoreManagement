using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.Interfaces.IServices
{
    public interface IInvoiceService
    {
        public Task<InvoiceDTO> CreateAsync(InvoiceDTO invoice);
        public Task<List<InvoiceDTO>> GetAllUnpaidAsync(int id);
        public Task<InvoiceDTO> GetInvoiceUnpaidById(int idStore, int idTable);
        public Task<List<InvoiceDTO>> GetAllPaidAsync(int idStore);
        public Task<InvoiceDTO> UpdateAsync(int id);
        public Task<InvoiceDTO> GetInvoicePaidById(int id);
        public Task<InvoiceDTO> GetInvoiceById(int id);
    }
}
