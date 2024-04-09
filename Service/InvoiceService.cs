using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepo = invoiceRepository;
            _mapper = mapper;
        }
        public async Task<InvoiceDTO> CreateAsync(InvoiceDTO invoiceDTO)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDTO);
            var result = await _invoiceRepo.CreateAsync(invoice);
            return _mapper.Map<InvoiceDTO>(result);
        }

        public async Task<List<InvoiceDTO>> GetAllPaidAsync(int idStore)
        {
            var list = await _invoiceRepo.GetAllPaidAsync(idStore);
            return _mapper.Map<List<InvoiceDTO>>(list);
        }

        public async Task<List<InvoiceDTO>> GetAllUnpaidAsync(int id)
        {
            var list = await _invoiceRepo.GetAllUnpaidAsync(id);
            return _mapper.Map<List<InvoiceDTO>>(list);
        }

        public async Task<InvoiceDTO> GetInvoiceById(int id)
        {
            var invoice = await _invoiceRepo.GetInvoiceById(id);
            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task<InvoiceDTO> GetInvoicePaidById(int idStore, int idTable, bool incluDeleted = false)
        {
            var invoice = await _invoiceRepo.GetInvoicePaidById(idStore, idTable);
            return _mapper.Map<InvoiceDTO>(invoice);
        }


        public async Task<InvoiceDTO> GetInvoicePaidById(int id)
        {
            var invoice = await _invoiceRepo.GetInvoicePaidById(id);
            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task<InvoiceDTO> GetInvoiceUnpaidById(int idStore, int idTable)
        {
            var invoice = await _invoiceRepo.GetInvoiceUnpaidById(idStore, idTable);
            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task<InvoiceDTO> UpdateAsync(int id)
        {
            var result = await _invoiceRepo.UpdateAsync(id);
            return _mapper.Map<InvoiceDTO>(result);
        }
    }
}
