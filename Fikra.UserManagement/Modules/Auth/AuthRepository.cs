using Dapper;
using Fikra.UserManagement.Constants;
using Fikra.UserManagement.Modules.Auth.DTO;
using Fikra.UserManagement.Modules.Users;
using Fikra.UserManagement.Modules.Users.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fikra.UserManagement.Modules.Auth
{
    public class AuthRepository : IAuthRepository
    {
        #region Constructor
        private readonly string ConnectionString;
        private readonly string ValidIssuer;
        private readonly string JWTKey;
        private readonly IUserManager _userManager;

        public AuthRepository(IConfiguration configuration, IUserManager userManager)
        {
            ConnectionString = configuration.GetConnectionString("default");
            ValidIssuer = configuration.GetSection("ValidIssuer").Value;
            JWTKey = configuration.GetSection("Key").Value;
            _userManager = userManager;
        }
        #endregion

        #region Methods
        public async Task<LoginResponseDTO> LoginUser(LoginCredentials credentials)
        {
            var user = await _userManager.UserGetByEmail(credentials.Email);

            if (user == null) return new LoginResponseDTO { Message = "Incorrect Credentials" };
            if (user.UserIsBanned != false) return new LoginResponseDTO { Message = "User Is Banned" };
            if (user.UserIsEnabled != true) return new LoginResponseDTO { Message = "User Is Disabled" };

            var enteredPassword = Helper.CreateMD5(credentials.Password);

            if (enteredPassword == user.UserPasswordHash)
            {

                var userPermissions = await PermissionsGetAllByUserID(user.UserID);
                var JWT = GenerateJWT(user, userPermissions);

                var Response = new LoginResponseDTO
                {
                    JWT = JWT,
                    UserFirstName = user.UserFirstname,
                    UserLastName = user.UserLastname,
                    UserEmail = user.UserEmail,
                    UserID = user.UserID,
                    RoleID = user.UserRoleID,
                    RoleName = user.UserRoleName,
                    Message = "Successful Login"
                };
                return Response;
            }
            else
            {
                return new LoginResponseDTO { Message = "Incorrect Credentials" };
            }
        }

        public async Task RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var user = await _userManager.UserGetByEmail(registerUserDTO.UserEmail);
            if (user == null)
            {
                var UserRoleID = await RoleGetByIntCode(RoleCodes.User);

                var par = new DynamicParameters();
                par.Add("UserFirstname", registerUserDTO.UserFirstname);
                par.Add("UserLastname", registerUserDTO.UserLastname);
                par.Add("UserEmail", registerUserDTO.UserEmail.ToLower());
                par.Add("UserImageBase64", registerUserDTO.UserImageBase64);
                par.Add("UserPasswordHash", Helper.CreateMD5(registerUserDTO.Password));
                par.Add("UserRoleID", UserRoleID);

                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.ExecuteAsync(
                        nameof(RegisterUser),
                        par,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            else
            {
                throw new Exception($"User with email {user.UserEmail} already exists");
            }
        }

        public Task ResetPassword(string email)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods

        private async Task<IEnumerable<PermissionDto>> PermissionsGetAllByUserID(int? UserID)
        {
            IEnumerable<PermissionDto> permissions;
            using (var connection = new SqlConnection(ConnectionString))
            {
                permissions = await connection.QueryAsync<PermissionDto>(
                    nameof(PermissionsGetAllByUserID),
                    new { UserID },
                    commandType: CommandType.StoredProcedure
                );
            }

            return permissions;
        }

        private string GenerateJWT(UserGetByEmailForAuthDto user, IEnumerable<PermissionDto> permissions)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>();

            foreach (var item in permissions)
            {
                claims.Add(new Claim(type: item.PermissionName!, value: item.PermissionName));
            }

            claims.Add(new Claim(type: ClaimTypes.Email, value: user.UserEmail));
            claims.Add(new Claim(type: "UserID", value: user.UserID.ToString()));
            claims.Add(new Claim(type: ClaimTypes.Role, value: user.UserRoleName));

            var token = new JwtSecurityToken(
              ValidIssuer,
              ValidIssuer,
              claims: claims,
              expires: DateTime.Now.AddDays(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<int> RoleGetByIntCode(int? RoleIntCode)
        {
            int RoleID;
            using (var connection = new SqlConnection(ConnectionString))
            {
                RoleID = (await connection.QuerySingleOrDefaultAsync<RoleGetByIntCodeDTO>(
                    nameof(RoleGetByIntCode),
                    new { RoleIntCode },
                    commandType: CommandType.StoredProcedure
                )).RoleID;
            }
            return RoleID;
        }
        #endregion


    }
}
