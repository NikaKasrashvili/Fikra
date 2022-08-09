using Fikra.UserManagement.Modules.Roles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Roles
{
    public interface IRoleRepository
    {
        Task RolesCreate(RolesCreateDTO rolesCreate);
        Task RolesUpdate(RolesUpdateDTO rolesUpdate);
        Task RolesDeleteByID(int roleID);
        Task<IEnumerable<RolesGetAllDTO>> RolesGetAll();
        Task<RoleGetByIDDTO> RoleGetByID(int RoleID);
        Task RoleAddUpdatePermissions(List<RolePermissionDTO> rolePermissionDTOs, int RoleID);
    }
}
