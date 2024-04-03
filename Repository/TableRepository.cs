using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationDbContext _context;
        public TableRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Table> Create(Table table)
        {
            var newTable = new Table()
            {
                Status = table.Status,
                StoreID = table.StoreID,
                CreateTime = table.CreateTime = DateTime.Now,
            };
            _context.Add(newTable);
            await _context.SaveChangesAsync();
            return newTable;
        }

        public async Task Delete(int id, bool incluDeleted = false)
        {
            var table = _context.Tables.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefault();
            _context.Remove(table);
            await _context.SaveChangesAsync();
        }

        public async Task<Table> Edit(int id, Table table, bool incluDeleted = false)
        {
            var tables = await _context.Tables.FindAsync(id);
            if(tables == null)
            {
                return null;
            }
            tables.Id = id;
            tables.Status = table.Status;
            tables.CreateTime = DateTime.Now;
            tables.StoreID = table.StoreID;
            await _context.SaveChangesAsync();
            return tables;
        }

        public async Task<List<Table>> GetAll(bool incluDeleted = false)
        {
            var list = new List<Table>();
            list = await _context.Tables.Include("Store").ToListAsync();
            return list.ToList();
        }

        public Task<Table> GetById(int id, bool incluDeleted = false)
        {
            var table = _context.Tables.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefaultAsync();
            return table;

        }
    }
}
