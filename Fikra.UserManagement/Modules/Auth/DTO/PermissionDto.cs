using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Auth.DTO
{
    public class PermissionDto
    {
        public int? PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public string PermissionDateCreated { get; set; }
    }
}
