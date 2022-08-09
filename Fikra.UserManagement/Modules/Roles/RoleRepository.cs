using Dapper;
using Fikra.UserManagement.Modules.Roles.DTO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fikra.UserManagement.Modules.Roles
{
    public class RoleRepository : IRoleRepository
    {
        #region Constructor
        private readonly string ConnectionString;

        public RoleRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("default");
        }


        #endregion

        #region Methods
        public async Task RolesCreate(RolesCreateDTO rolesCreate)
        {
            var par = new DynamicParameters();
            par.Add("RoleName", rolesCreate.RoleName);
            par.Add("RoleIsDeletable", rolesCreate.RoleIsDeletable);
            par.Add("RoleIntCode", rolesCreate.RoleIntCode);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(RolesCreate),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<RoleGetByIDDTO> RoleGetByID(int RoleID)
        {
            RoleGetByIDDTO roleByID;
            using (var connection = new SqlConnection(ConnectionString))
            {
                roleByID = await connection.QuerySingleOrDefaultAsync<RoleGetByIDDTO>(
                    nameof(RoleGetByID),
                    new { RoleID = RoleID },
                    commandType: CommandType.StoredProcedure
                );
            }
            return roleByID;

        }
        public async Task RolesDeleteByID(int roleID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(RolesDeleteByID),
                    new { RoleID = roleID },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<IEnumerable<RolesGetAllDTO>> RolesGetAll()
        {
            IEnumerable<RolesGetAllDTO> allRoles;

            using (var connection = new SqlConnection(ConnectionString))
            {
                allRoles = await connection.QueryAsync<RolesGetAllDTO>(
                    nameof(RolesGetAll),
                    new { },
                    commandType: CommandType.StoredProcedure
                );
            }
            return allRoles;
        }

        public async Task RolesUpdate(RolesUpdateDTO rolesUpdate)
        {
            var par = new DynamicParameters();
            par.Add("RoleName", rolesUpdate.RoleName);
            par.Add("RoleID", rolesUpdate.RoleID);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(RolesUpdate),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }



        

        public async Task RoleAddUpdatePermissions(List<RolePermissionDTO> rolePermissionDTOs, int RoleID)
        {
            await PermissionsDeleteByRoleID(RoleID);

            if (rolePermissionDTOs.Any())
            {
                foreach (var item in rolePermissionDTOs)
                {
                    await RoleAddPermission(item);
                }
            }
        }



        #endregion

        #region Private Methods
        private async Task PermissionsDeleteByRoleID(int RoleID)
        {
            var par = new DynamicParameters();
            par.Add("RoleID", RoleID);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(PermissionsDeleteByRoleID),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task RoleAddPermission(RolePermissionDTO rolePermissionDTO)
        {
            var par = new DynamicParameters();
            par.Add("RoleID", rolePermissionDTO.RoleID);
            par.Add("PermissionID", rolePermissionDTO.PermissionID);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(RoleAddPermission),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        #endregion

    }
}
