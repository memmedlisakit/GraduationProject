using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using System.Data;
using System.Data.SqlClient;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class OrderController : Controller
    {
        string cs = LoginLogoutController.cs;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [UserRoleAuthorizeFilter]
        public ActionResult Index()
        {
            return View(select());
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public void Order_Update(int order_id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "UPDATE Orders SET order_status = (CASE WHEN order_status = 1 THEN 0 ELSE 1 END) WHERE order_id = " + order_id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public void Delete_Order(int order_id)
        {
             using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "DELETE FROM Orders WHERE order_id = " + order_id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
        }
         
        List<Order> select()
        {
            List<Order> orders = new List<Order>();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT order_id, user_username, user_email, user_img, order_date, pro_name, order_product_quantity, order_total_price, order_status FROM Orders INNER JOIN Users ON order_user_id = user_id INNER JOIN Products ON pro_id = order_product_id";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                orders.Add(new Order()
                {
                    order_id = Convert.ToInt32(row["order_id"]),
                    user_username = row["user_username"].ToString(),
                    user_email = row["user_email"].ToString(),
                    user_img = row["user_img"].ToString(),
                    order_date = Convert.ToDateTime(row["order_date"]),
                    pro_name =row["pro_name"].ToString(),
                    order_product_quantity = Convert.ToInt32(row["order_product_quantity"]),
                    order_total_price = Convert.ToDouble(row["order_total_price"]),
                    order_status = Convert.ToBoolean(row["order_status"])
                });
            }


            return orders;
        }
         
    }
}