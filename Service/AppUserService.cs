using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly IMapper _mapper;
        public AppUserService(IAppUserRepository appUserRepository, IMapper mapper)
        {
            this.appUserRepository = appUserRepository;
            _mapper = mapper;
        }
        public async Task<AppUserDTO> CreateAsync(AppUserDTO appUserDTO)
        {
            var appUser = _mapper.Map<AppUser>(appUserDTO);
            await appUserRepository.CreateAsync(appUser);
            appUser = await appUserRepository.GetById(appUser.Id);
            var appUserDtoResult = _mapper.Map<AppUserDTO>(appUser);
            return appUserDtoResult;
        }

        public async Task<bool> Delete(int id, bool incluDeleted = false)
        {
            await appUserRepository.Delete(id, incluDeleted);
            return true;
        }

        public async Task<List<AppUserDTO>> GetAllAsync()
        {
            var list = await appUserRepository.GetAll();
            return _mapper.Map<List<AppUserDTO>>(list);
        }

        public async Task<AppUserDTO> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var user = await appUserRepository.GetById(id,incluDeleted);
            return _mapper.Map<AppUserDTO>(user);
        }

        public async Task<List<AppUserDTO>> GetByNameAsync(string name )
        {
            var list = await appUserRepository.GetByNameAsync(name);
            return _mapper.Map<List<AppUserDTO>>(list);
        }

        public async Task<AppUserDTO> UpdateAsync(AppUserDTO appUserDTO, bool incluDeleted = false)
        {
            var appUser = _mapper.Map<AppUser>(appUserDTO);
            await appUserRepository.UpdateAsync(appUser,incluDeleted);
            return _mapper.Map<AppUserDTO>(appUser);
        }
    }
}
