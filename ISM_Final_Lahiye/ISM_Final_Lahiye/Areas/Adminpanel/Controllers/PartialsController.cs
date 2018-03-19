using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class PartialsController : Controller
    {
        string cs = LoginLogoutController.cs;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        public PartialViewResult Navbar()
        {
            ViewBag.user_vart_count = user_cart_count();
            return PartialView(select(LoginLogoutController.Logined_user.user_id));
        }
         
        Users select(int id)
        {
            dt.Clear();
            Users user = new Users();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT * FROM Users WHERE user_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            user.user_id = Convert.ToInt32(dt.Rows[0]["user_id"]);
            user.user_username = dt.Rows[0]["user_username"].ToString();
            user.user_email = dt.Rows[0]["user_email"].ToString();
            user.user_img = dt.Rows[0]["user_img"].ToString(); 
            return user;
        }


        int user_cart_count()
        {
            dt.Clear();
            using (SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT cart_user_id FROM Cart WHERE cart_user_id = " + LoginLogoutController.Logined_user.user_id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
                 
            } 
            return dt.Rows.Count;
        }
    }
}