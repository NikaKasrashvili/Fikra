using Dapper;
using Fikra.UserManagement.Modules.Auth.DTO;
using Fikra.UserManagement.Modules.Users.DTO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Fikra.UserManagement.Modules.Users
{
    public class UserManager : IUserManager
    {
        #region Constructor
        private readonly string ConnectionString;
        public UserManager(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("default");
        }
        #endregion


        #region Methods
        public async Task UserChangeBannedStatus(UserChangeBannedStatusDTO userChangeBannedStatus)
        {

            var par = new DynamicParameters();
            par.Add("UserID", userChangeBannedStatus.UserID);
            par.Add("UserIsBanned", userChangeBannedStatus.UserIsBanned);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                   nameof(UserChangeBannedStatus),
                   par,
                   commandType: CommandType.StoredProcedure
               );
            }

        }

        public Task DeleteUser(int UserID)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRoleGetByUserIDDto> UserRoleGetByUserID(int UserID)
        {
            UserRoleGetByUserIDDto Role;

            using (var connection = new SqlConnection(ConnectionString))
            {
                Role = await connection.QuerySingleOrDefaultAsync<UserRoleGetByUserIDDto>(
                    nameof(UserRoleGetByUserID),
                    new { UserID },
                    commandType: CommandType.StoredProcedure
                );
            }

            return Role;
        }

        public async Task RequestPasswordReset(string UserEmail)
        {
            var par = new DynamicParameters();
            par.Add("UserEmail", UserEmail);
            par.Add("ResetPasswordHash", GenerateRandomString());

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(RequestPasswordReset),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var par = new DynamicParameters();
            par.Add("NewPasswordHash", Helper.CreateMD5(resetPasswordDTO.NewPasswordHash));
            par.Add("PasswordResetHash", resetPasswordDTO.PasswordResetHash);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(ResetPassword),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task UserChangePassword(UserChangePasswordDto userChangePassword)
        {
            var par = new DynamicParameters();
            par.Add("UserPasswordHash", Helper.CreateMD5(userChangePassword.NewPassword));
            par.Add("UserID", userChangePassword.UserID);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(UserChangePassword),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }

        }

        public async Task UserUpdate(UserUpdateDto updateUserDto)
        {
            var par = new DynamicParameters();
            par.Add("UserID", updateUserDto.UserID);
            par.Add("UserFirstName", updateUserDto.UserFirstname);
            par.Add("UserLastName", updateUserDto.UserLastname);
            par.Add("UserImageBase64", updateUserDto.UserImageBase64);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(UserUpdate),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }

        }


        public async Task<IEnumerable<UsersGetAllDTO>> UsersGetAll()
        {
            IEnumerable<UsersGetAllDTO> AllUsers;

            using (var connection = new SqlConnection(ConnectionString))
            {
                AllUsers = await connection.QueryAsync<UsersGetAllDTO>(
                    nameof(UsersGetAll),
                    new { },
                    commandType: CommandType.StoredProcedure
                );

                return AllUsers;
            }

        }

        public async Task UserRoleChange(UserRoleChangeDTO userRoleChange)
        {
            var par = new DynamicParameters();
            par.Add("UserRoleID", userRoleChange.UserRoleID);
            par.Add("UserID", userRoleChange.UserID);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(UserRoleChange),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<UserGetByEmailForAuthDto> UserGetByEmail(string UserEmail)
        {
            UserGetByEmailForAuthDto user;
            var par = new DynamicParameters();
            par.Add("Input", UserEmail);

            using (var connection = new SqlConnection(ConnectionString))
            {
                user = await connection.QuerySingleOrDefaultAsync<UserGetByEmailForAuthDto>(
                    nameof(UserGetByEmail),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }

            return user;
        }


        public async Task<UserGetByEmailDTO> UserGetByEmailAdminPanel(string UserEmail)
        {
            UserGetByEmailDTO user;
            var par = new DynamicParameters();
            par.Add("UserEmail", UserEmail);

            using (var connection = new SqlConnection(ConnectionString))
            {
                user = await connection.QuerySingleOrDefaultAsync<UserGetByEmailDTO>(
                    nameof(UserGetByEmailAdminPanel),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }

            return user;
        }
        #endregion

        #region Private Methods

        private string GenerateRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[200];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        #endregion
    }
}
