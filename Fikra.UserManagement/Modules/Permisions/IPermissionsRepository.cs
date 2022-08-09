using Fikra.UserManagement.Modules.Permisions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Permisions
{
    public interface IPermissionsRepository
    {
        Task<IEnumerable<PermissionsGetAllDTO>> PermissionsGetAll();
        Task<IEnumerable<PermissionsGetByRoleIDDTO>> PermissionsGetByRoleID(int RoleID);
        Task<List<RolePermissionsDTO>> RolePermissions(int RoleID);
    }
}
