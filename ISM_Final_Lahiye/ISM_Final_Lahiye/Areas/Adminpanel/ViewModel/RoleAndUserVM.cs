using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class RoleAndUserVM
    {
        public List<Role> roles { get; set; }
        public List<Users> users { get; set; }
    }
}