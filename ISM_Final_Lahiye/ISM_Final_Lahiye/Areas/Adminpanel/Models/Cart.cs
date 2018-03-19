using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class Cart
    {
        public int cart_id { get; set; }
        public int cart_user_id { get; set; }
        public int pro_id { get; set; }
        public string pro_name { get; set; }
        public string brand_name { get; set; }
        public double pro_price { get; set; }
        public int cart_product_quantity { get; set; }
        public string pro_color { get; set; }
        public string pro_img { get; set; }
    }
}