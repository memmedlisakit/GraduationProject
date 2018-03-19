using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class RoleAndUser
    {
        public int p_id { get; set; }
        public int user_id { get; set; }
        public int role_id { get; set; }
        public string user_username { get; set; }
        public string user_email { get; set; }
        public string user_img { get; set; }
        public string role_name { get; set; }
    }
}