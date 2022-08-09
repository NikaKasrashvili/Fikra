using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Users.DTO
{
    public class UserChangePasswordDto
    {
        public int? UserID { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
    }
}
