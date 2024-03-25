using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Store> CreateAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task Delete(int id, bool incluDeleted = false)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Store>> GetAll()
        {
            var list = new List<Store>();
            list = await _context.Stores.ToListAsync();
            return list.ToList();
        }

        public Task<Store> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var store = _context.Stores.Where(obj=>obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefaultAsync();
            return store;
        }

        public async Task<IList<Store>> GetByNameAsync(string name, bool incluDeleted = false)
        {
            var list = await _context.Stores.Where(p=>p.StoreName.Contains(name)).ToListAsync();
            return list;
        }

        public async Task<Store> UpdateAsync(Store store, bool includeDeletd = false)
        {
            var storeUpdate = await _context.Stores.FirstOrDefaultAsync(obj => obj.Id == store.Id && obj.IsDeleted == includeDeletd);
            if(storeUpdate != null)
            {
                storeUpdate.StoreName = store.StoreName;
                storeUpdate.AddressStore = store.AddressStore;
                await _context.SaveChangesAsync();
            }
            return storeUpdate;
        }
    }
}
