using Dapper;
using Fikra.UserManagement.Modules.Permisions.DTO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fikra.UserManagement.Modules.Permisions
{
    public class PermissionsRepository : IPermissionsRepository
    {
        #region Constructor
        private readonly string ConnectionString;

        public PermissionsRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("default");
        }


        #endregion

        #region Methods
        public async Task<IEnumerable<PermissionsGetAllDTO>> PermissionsGetAll()
        {
            IEnumerable<PermissionsGetAllDTO> allPermisions;

            using (var connection = new SqlConnection(ConnectionString))
            {
                allPermisions = await connection.QueryAsync<PermissionsGetAllDTO>(
                    nameof(PermissionsGetAll),
                    new { },
                    commandType: CommandType.StoredProcedure
                );
            }
            return allPermisions;
        }

        public async Task<IEnumerable<PermissionsGetByRoleIDDTO>> PermissionsGetByRoleID(int RoleID)
        {
            IEnumerable<PermissionsGetByRoleIDDTO> Permissions;

            using (var connection = new SqlConnection(ConnectionString))
            {
                Permissions = await connection.QueryAsync<PermissionsGetByRoleIDDTO>(
                    nameof(PermissionsGetByRoleID),
                    new { RoleID = RoleID },
                    commandType: CommandType.StoredProcedure
                );
            }
            return Permissions;
        }

        public async Task<List<RolePermissionsDTO>> RolePermissions(int RoleID)
        {
            List<RolePermissionsDTO> result = new List<RolePermissionsDTO>();

            var allPermissions = await PermissionsGetAll();
            var rolePermissions = await PermissionsGetByRoleID(RoleID);

            foreach (var item in allPermissions)
            {
                if (rolePermissions.Any(x => x.PermissionID == item.PermissionID))
                {
                    result.Add(new RolePermissionsDTO
                    {
                        PermissionID = item.PermissionID,
                        PermissionName = item.PermissionName,
                        HasRolePermission = true
                    });
                }
                else
                {
                    result.Add(new RolePermissionsDTO
                    {
                        PermissionID = item.PermissionID,
                        PermissionName= item.PermissionName,
                        HasRolePermission = false
                    });
                }
            }

            return result;
        }


    }
    #endregion
}

