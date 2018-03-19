using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using System.IO;
using ISM_Final_Lahiye.Areas.Adminpanel.Public;
using System.Configuration;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class UserController : Controller
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
        public ActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string query = "DELETE [Users] WHERE [user_id] = " + id;
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                TempData["delete_error_message"] = "You can not delete this User because this User connected to a Role";
                return RedirectToAction("Index");
            }
            
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Insert()
        {
            return View();
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Insert(Users user, HttpPostedFileBase user_img)
        {
            string hashed_password = Settings.Hash_Password(user.user_password);
            string file_name = null;
            if (user_img != null) 
            {
                file_name = DateTime.Now.ToString("ddMMyyyyHHmmssffftt") + user_img.FileName;
                string file_path = Path.Combine(Server.MapPath("/Uploads/"), file_name);
                user_img.SaveAs(file_path);
            } 
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "INSERT INTO [Users] (user_username, user_email, user_password, user_img) VALUES ('" + user.user_username + "', '" + user.user_email + "', '" + hashed_password + "', '" + file_name + "')";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Update(int id)
        {
            return View(select(id));
        }

        [UserRoleAuthorizeFilter]
        [HttpPost] 
        public ActionResult Update(int id, Users user, HttpPostedFileBase user_img)
        {
            update(id, user, user_img);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void Update_password(string new_password)
        {
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "UPDATE Users SET user_password = '"+Settings.Hash_Password(new_password)+"' WHERE user_id = " + LoginLogoutController.Logined_user.user_id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
        }
         
        List<Users> select()
        {
            List<Users> users = new List<Users>();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT * FROM [Users]";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                users.Add(new Users()
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    user_username = row["user_username"].ToString(),
                    user_email = row["user_email"].ToString(),
                    user_img = row["user_img"].ToString()
                });
            }
            return users;
        }

        Users select(int id)
        {
            Users user = new Users();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM [Users] WHERE [user_id] = " + id;
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

        void update(int id, Users user, HttpPostedFileBase user_img)
        {
            string query = "UPDATE Users SET user_username = '" + user.user_username + "', user_email = '" + user.user_email + "' WHERE user_id = " + id;

            string file_name = null;
            if (user_img != null)
            {
                file_name = DateTime.Now.ToString("ddMMyyyyHHmmssffftt") + user_img.FileName;
                string file_path = Path.Combine(Server.MapPath("/Uploads/"), file_name);
                user_img.SaveAs(file_path);
                query = "UPDATE Users SET user_username = '" + user.user_username + "', user_email = '" + user.user_email + "', user_img = '" + file_name + "' WHERE user_id = " + id;

            }
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
        }
         
        public ActionResult check_username(string username)
        { 
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Users WHERE user_username = '" + username + "'";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            } 
            if (dt.Rows.Count <= 0)
            {
                username = "Username is available";
                return Json(username);
            }
            else
            {
                username = "\"" + username + "\" username already exists";
                return Json(username);
            } 
        } 
    } 
}