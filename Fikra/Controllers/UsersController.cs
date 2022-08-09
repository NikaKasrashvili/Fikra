using Fikra.UserManagement;
using Fikra.UserManagement.Modules.Auth.DTO;
using Fikra.UserManagement.Modules.Users;
using Fikra.UserManagement.Modules.Users.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Fikra.Filters.ClaimAuthorizationFilter;

namespace Fikra.Controllers
{
    [Authorize]
    [Route("Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Constructor
        private readonly IUserManager userManager;

        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        #endregion

        #region Actions
        [HttpPut("update-user-Banned-Status")]
        [RequireClaim("UserChangeBannedStatus")]
        public async Task<ActionResult> UserChangeBannedStatus(UserChangeBannedStatusDTO userChangeBannedStatus)
        {
            await userManager.UserChangeBannedStatus(userChangeBannedStatus);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("request-reset-password")]
        public async Task RequestPasswordReset(string email)
        {
            await userManager.RequestPasswordReset(email);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            await userManager.ResetPassword(resetPasswordDTO);
            return Ok();
        }


        [HttpPost("change-password")]
        public async Task<ActionResult> UserChangePassword(UserChangePasswordDto userChangePassword)
        {
            var UserEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var CurrentPasswordHash = (await userManager.UserGetByEmail(UserEmail))?.UserPasswordHash;

            if (Helper.CreateMD5(userChangePassword.CurrentPassword) == CurrentPasswordHash)
            {
                await userManager.UserChangePassword(userChangePassword);
                return Ok();
            }

            return BadRequest();
        }


        [HttpGet("Get-All-Users")]
        [RequireClaim("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UsersGetAllDTO>>> UsersGetAll()
        {
            var result = await userManager.UsersGetAll();
            return Ok(result);
        }


        [HttpPut("Update-User")]
        public async Task<ActionResult> UserUpdate(UserUpdateDto updateUser)
        {
            var UserID = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);

            if (UserID == updateUser.UserID || HttpContext.User.Claims.Any(x => x.Value == "Admin"))
            {
                await userManager.UserUpdate(updateUser);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }

        }


        [HttpPut("Change-User-Role")]
        [RequireClaim("UserRoleChange")]
        public async Task<ActionResult> UserRoleChange(UserRoleChangeDTO userRoleChange)
        {
            await userManager.UserRoleChange(userRoleChange);
            return Ok();

        }

        [HttpGet("Get-User-By-Email/{UserEmail}")]
        [RequireClaim("UserGetByEmailAdminPanel")]
        public async Task<ActionResult<UserGetByEmailDTO>> GetUserByEmail(string UserEmail)
        {
            var result = await userManager.UserGetByEmailAdminPanel(UserEmail);
            return Ok(result);
        }
        #endregion
    }
}
