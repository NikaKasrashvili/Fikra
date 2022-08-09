using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Users.DTO
{
    public class UserUpdateDto
    {
        public int? UserID { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public string UserImageBase64 { get; set; }
    }
}
