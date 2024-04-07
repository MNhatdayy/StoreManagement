using StoreManagement.DTO;
using StoreManagement.DTO.AuthDTO;

namespace StoreManagement.Interfaces.IServices

{
    public interface IAuthenticationService
    {
        Task<AppUserDTO> Login(LoginDTO loginDTO);
        Task<bool> Register(RegisterDTO registerDTO);
        Task<bool> ChangePassword(ChangePasswordDTO changePass);
    }
}
