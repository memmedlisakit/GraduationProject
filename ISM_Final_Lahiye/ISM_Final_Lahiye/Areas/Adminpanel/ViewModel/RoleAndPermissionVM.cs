using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class RoleAndPermissionVM
    {
        public List<Role> roles { get; set; }
        public List<Permission> permissions { get; set; }
        public List<PermissionAndRole> permission_id_and_role_id { get; set; }
        public Role role { get; set; }
    }
}