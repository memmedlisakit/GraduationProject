using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class Users
    {
        public int user_id { get; set; }
        public string user_username { get; set; }
        public string user_email { get; set; }
        public string user_password { get; set; }
        public string user_img { get; set; }
    }
}