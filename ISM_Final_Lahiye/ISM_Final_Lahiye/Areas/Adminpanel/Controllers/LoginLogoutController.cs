using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using ISM_Final_Lahiye.Areas.Adminpanel.Public;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    public class LoginLogoutController : Controller
    {
        public static Users Logined_user = new Users();
        public static string cs = WebConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        { 
            string email = form["user_email"];
            string password = Settings.Hash_Password(form["user_password"].ToString());
             
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    string query = "SELECT * FROM Users WHERE user_email = '" + email + "' AND user_password = '" + password + "'";
                    SqlCommand com = new SqlCommand(query, conn);
                    da.SelectCommand = com;
                    da.Fill(dt);
                    if (dt.Rows.Count <= 0)
                    {
                        ViewBag.error = "Email or Password incorrect";
                        return View();
                    } 
                    Logined_user.user_id = Convert.ToInt32(dt.Rows[0]["user_id"]);
                    Logined_user.user_username = dt.Rows[0]["user_username"].ToString();
                    Logined_user.user_email = dt.Rows[0]["user_email"].ToString();
                    Logined_user.user_password = dt.Rows[0]["user_password"].ToString();
                    Logined_user.user_img = dt.Rows[0]["user_img"].ToString();


                    // Hash sessions username and password
                    Session["username"] = Settings.Hash_Password(Logined_user.user_username);
                    Session["password"] = Settings.Hash_Password(Logined_user.user_password);
                }   
            UserRoleAuthorizeFilter.permission_warning = null;
            return RedirectToAction("Index", "Dashboard");
        }


        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["password"] = null;
            Logined_user.user_id = 0;
            Logined_user.user_username = null;
            Logined_user.user_email = null;
            Logined_user.user_password = null;
            Logined_user.user_img = null;
            return RedirectToAction("Login");
        }
    }
}