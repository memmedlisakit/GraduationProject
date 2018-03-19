using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Models
{
    public class Order
    {
        public int order_id { get; set; }
        public string user_username { get; set; }
        public string user_email { get; set; }
        public string user_img { get; set; } 
        public string pro_name { get; set; }
        public DateTime order_date { get; set; }
        public int order_product_quantity { get; set; }
        public double order_total_price { get; set; }
        public bool order_status { get; set; }
    }
}