using ISM_Final_Lahiye.Areas.Adminpanel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class DashboardController : Controller
    {
        string cs = LoginLogoutController.cs;

        [UserRoleAuthorizeFilter]
        public ActionResult Index()
        {
            ViewBag.userCount = user_count();
            ViewBag.orderCount = order_count();
            ViewBag.productCount = product_count();
            return View();
        }

        int user_count()
        {
            int count;
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT COUNT(user_id) FROM Users";
                SqlCommand com = new SqlCommand(query, conn);
                count =  Convert.ToInt32(com.ExecuteScalar());
                conn.Close();
            }
            return count;
        }

        int order_count()
        {
            int count;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT COUNT(order_id) FROM Orders";
                SqlCommand com = new SqlCommand(query, conn);
                count = Convert.ToInt32(com.ExecuteScalar());
                conn.Close();
            }
            return count;
        }

        int product_count()
        {
            int count;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT COUNT(pro_id) FROM Products";
                SqlCommand com = new SqlCommand(query, conn);
                count = Convert.ToInt32(com.ExecuteScalar());
                conn.Close();
            }
            return count;
        }
    }
}