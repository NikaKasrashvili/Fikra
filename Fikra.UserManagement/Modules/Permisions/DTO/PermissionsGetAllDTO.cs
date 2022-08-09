using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Permisions.DTO
{
    public class PermissionsGetAllDTO
    {
        public int? PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
    }
}
