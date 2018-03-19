using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class Permission
    {
        public int per_id { get; set; }
        public string per_link { get; set; }
        public string per_controller { get; set; }
        public string per_action { get; set; }

    }
}