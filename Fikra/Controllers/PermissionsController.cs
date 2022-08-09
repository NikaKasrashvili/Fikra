using Fikra.UserManagement.Modules.Permisions;
using Fikra.UserManagement.Modules.Permisions.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Fikra.Filters.ClaimAuthorizationFilter;

namespace Fikra.Controllers
{
    [Authorize]
    [Route("Permisions")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        #region Constructor
        private readonly IPermissionsRepository permisionsRepository;
        public PermissionsController(IPermissionsRepository permisionsRepository)
        {
            this.permisionsRepository = permisionsRepository;
        }
        #endregion

        #region Actions
        [HttpGet("get-all")]
        [RequireClaim("PermissionsGetAll")]
        public async Task<ActionResult<List<PermissionsGetAllDTO>>> PermissionsGetAll()
        {
            var result = await permisionsRepository.PermissionsGetAll();
            return Ok(result);
        }

        [HttpGet("get-role-permissions/{RoleID}")]
        [RequireClaim("RoleGetPermissions")]
        public async Task<ActionResult<IEnumerable<RolePermissionsDTO>>> RoleGetPermissions(int RoleID)
        {
            var result = await permisionsRepository.RolePermissions(RoleID);
            return Ok(result);
        }
        #endregion
    }
}
