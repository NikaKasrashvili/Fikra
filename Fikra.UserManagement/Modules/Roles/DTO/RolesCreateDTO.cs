﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.UserManagement.Modules.Roles.DTO
{
    public class RolesCreateDTO
    {
        public string RoleName { get; set; }
        public int? RoleIntCode { get; set; }
        public bool? RoleIsDeletable { get; set; }
    }
}
