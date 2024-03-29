using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Repository;

namespace StoreManagement.Service
{
    public class TableService : ITableService
    {
        private readonly ITableRepository tableRepository;
        private readonly IMapper _mapper;
        public TableService(ITableRepository tableRepository, IMapper mapper) 
        {
            this.tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<TableDTO> Create(TableDTO tableDTO)
        {
            var table = _mapper.Map<Table>(tableDTO); 
            var createdTable = await tableRepository.Create(table);
            return _mapper.Map<TableDTO>(createdTable);
        }

        public async Task<bool> Delete(int id, bool incluDeleted = false)
        {
            await tableRepository.Delete(id,incluDeleted);
            return true;
        }

        public async Task<TableDTO> Edit(int id, TableDTO tableDTO, bool incluDeleted = false)
        {
            var table = await tableRepository.Edit(id, _mapper.Map<Table>(tableDTO));
            if (table == null)
            {
                return null;
            }
            return _mapper.Map<TableDTO>(table);
        }

        public async Task<List<TableDTO>> GetAll()
        {
            var list = await tableRepository.GetAll();
            return _mapper.Map<List<TableDTO>>(list);
        }

        public async Task<TableDTO> GetById(int id, bool incluDeleted = false)
        {
            var table = await tableRepository.GetById(id, incluDeleted);
            return _mapper.Map<TableDTO>(table);
        }
    }
}
