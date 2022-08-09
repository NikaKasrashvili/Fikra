using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Permisions.DTO
{
    public class RolePermissionsDTO
    {
        public int? PermissionID { get; set; }
        public string PermissionName { get; set; }
        public bool? HasRolePermission { get; set; }
    }
}
