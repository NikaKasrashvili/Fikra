using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Users.DTO
{
    public class UserGetByEmailForAuthDto
    {
        public int? UserID { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public string UserEmail { get; set; }
        public int? UserRoleID { get; set; }
        public string UserRoleName { get; set; }
        public bool? UserIsBanned { get; set; }
        public string UserPasswordHash { get; set; }
        public bool? UserIsEnabled { get; set; }
        public string UserImageBase64 { get; set; }
    }
}
