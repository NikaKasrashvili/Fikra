using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Users.DTO
{
    public class UserChangeBannedStatusDTO
    {
        public int? UserID { get; set; }
        public bool? UserIsBanned { get; set; }
    }
}
