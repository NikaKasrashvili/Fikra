using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Auth.DTO
{
    public class ResetPasswordDTO
    {
        public string NewPasswordHash { get; set; }
        public string PasswordResetHash { get; set; }
    }
}
