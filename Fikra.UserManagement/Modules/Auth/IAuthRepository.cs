using Fikra.UserManagement.Modules.Auth.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Auth
{
    public interface IAuthRepository
    {
        Task RegisterUser(RegisterUserDTO registerUserDTO);
        Task ResetPassword(string email);
        Task<LoginResponseDTO> LoginUser(LoginCredentials credentials);

    }
}
