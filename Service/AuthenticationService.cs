using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StoreManagement.DTO;
using StoreManagement.DTO.AuthDTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Repository;

namespace StoreManagement.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authRepo;
        private readonly IMapper mapper;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, IMapper mapper)
        {
            _authRepo = authenticationRepository;
            this.mapper = mapper;
        }

        public async Task<bool> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if(changePasswordDTO == null ||
                string.IsNullOrEmpty(changePasswordDTO.Email) ||
                string.IsNullOrEmpty(changePasswordDTO.NewPassword) ||
                string.IsNullOrEmpty(changePasswordDTO.ConfirmPassword) ||
                string.IsNullOrEmpty(changePasswordDTO.ConfirmPassword) ||
                changePasswordDTO.ConfirmPassword != changePasswordDTO.NewPassword)  {
                return false;
            }
            var user = await _authRepo.FindByEmailAsync(changePasswordDTO.Email);
            if (user != null && user.Id > 0 && !user.IsDeleted)
            {
                var result = await _authRepo.ChangePasswordAsync(user, user.Password ,changePasswordDTO.NewPassword);
            }
            return false;
        }

        public async Task<AppUserDTO> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return null;
            }
            var user = await _authRepo.FindByEmailAsync(loginDTO.Email);
            if (user != null && user.Id > 0 && !user.IsDeleted)
            {
                if (await _authRepo.CheckPasswordAsync(user, loginDTO.Password))
                {
                    return mapper.Map<AppUserDTO>(user);
                }
            }
            return null;
        }

        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            if (registerDTO == null ||
                string.IsNullOrEmpty(registerDTO.Email) ||
                string.IsNullOrEmpty(registerDTO.Password) ||
                string.IsNullOrEmpty(registerDTO.ConfirmPassword) ||
                string.IsNullOrEmpty(registerDTO.FullName) ||
                registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return false;
            }

            var user = await _authRepo.FindByEmailAsync(registerDTO.Email);
            if (user == null)
            {
                var newUser = new AppUser { Name = registerDTO.FullName, Email = registerDTO.Email };
                var result = await _authRepo.CreateAsync(newUser, registerDTO.Password);
                if (result)
                {
                    if (newUser.Id > 0)
                    {
                        await _authRepo.AddToRoleAsync(newUser, 1);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
