using Fikra.UserManagement.Modules.Auth.DTO;
using Fikra.UserManagement.Modules.Users.DTO;

namespace Fikra.UserManagement.Modules.Users
{
    public interface IUserManager
    {
        Task<IEnumerable<UsersGetAllDTO>> UsersGetAll();
        Task<UserGetByEmailForAuthDto> UserGetByEmail(string usernameOrEmail);
        Task UserChangeBannedStatus(UserChangeBannedStatusDTO userChangeBannedStatus);
        Task DeleteUser(int UserID);
        Task UserUpdate(UserUpdateDto updateUserDto);
        Task UserRoleChange(UserRoleChangeDTO userRoleChange);

        Task<UserRoleGetByUserIDDto> UserRoleGetByUserID(int UserID);

        Task ResetPassword(ResetPasswordDTO resetPasswordDTO);

        Task RequestPasswordReset(string UserEmail);
        Task UserChangePassword(UserChangePasswordDto userChangePassword);
        Task <UserGetByEmailDTO> UserGetByEmailAdminPanel(string UserEmail);


    }
}
