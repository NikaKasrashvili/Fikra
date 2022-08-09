using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Auth.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string UserFirstname { get; set; }

        [Required]
        public string UserLastname { get; set; }
        public string UserImageBase64 { get; set; }


        [EmailAddress]
        public string UserEmail { get; set; }


        [MinLength(8)]
        public string Password { get; set; }
    }
}
