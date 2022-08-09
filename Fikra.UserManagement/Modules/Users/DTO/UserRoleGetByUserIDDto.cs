using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Users.DTO
{
    public class UserRoleGetByUserIDDto
    {
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
