using Fikra.UserManagement.Modules.Roles;
using Fikra.UserManagement.Modules.Roles.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Fikra.Filters.ClaimAuthorizationFilter;

namespace Fikra.Controllers
{
    [Authorize]
    [Route("Roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        #region Constructor
        private readonly IRoleRepository roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        #endregion

        #region Actions
        [HttpPost("Create-Role")]
        [RequireClaim("RolesCreate")]
        public async Task<ActionResult> RolesCreate(RolesCreateDTO rolesCreate)
        {
            await roleRepository.RolesCreate(rolesCreate);
            return Ok();
        }


        [HttpDelete("Delete-Role/{RoleID}")]
        [RequireClaim("RolesDelete")]
        public async Task<ActionResult> RolesDeleteByID(int RoleID)
        {
            var role = await roleRepository.RoleGetByID(RoleID);
            if (role.RoleIsDeletable == true)
            {
                await roleRepository.RolesDeleteByID(RoleID);
                return Ok();
            } else
            {
                throw new Exception("This Role Can't be Deleted");
            }

        }

        [HttpGet("get-all")]
        [RequireClaim("RolesGetAll")]
        public async Task<ActionResult<List<RolesGetAllDTO>>> RolesGetAll()
        {
            var result = await roleRepository.RolesGetAll();
            return Ok(result);
        }


        [HttpGet("get-by-id/{RoleID}")]
        [RequireClaim("RoleGetByID")]
        public async Task<ActionResult<RoleGetByIDDTO>> RoleGetByID(int RoleID)
        {
            var result = await roleRepository.RoleGetByID(RoleID);
            return Ok(result);
        }

        [HttpPut("update-role")]
        [RequireClaim("RolesUpdate")]
        public async Task<ActionResult> RolesUpdate(RolesUpdateDTO rolesUpdate)
        {
            await roleRepository.RolesUpdate(rolesUpdate);
            return Ok();
        }


        [HttpPut("update-role-permissions/{RoleID}")]
        [RequireClaim("RoleAddUpdatePermissions")]
        public async Task<ActionResult> RoleAddUpdatePermissions(List<RolePermissionDTO> rolePermissionsUpdate, int RoleID)
        {
            await roleRepository.RoleAddUpdatePermissions(rolePermissionsUpdate, RoleID);
            return Ok();
        }
        #endregion


    }
}
