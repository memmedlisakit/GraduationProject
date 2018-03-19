using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class Product
    {
        public int pro_id { get; set; }

        public string pro_name { get; set; }

        public int pro_quantity { get; set; }

        public decimal pro_price { get; set; } 

        public string pro_img { get; set; } 

        public string pro_color { get; set; }
        
        public string pro_status { get; set; } 

        public int pro_brand_id { get; set; } 
    }
}