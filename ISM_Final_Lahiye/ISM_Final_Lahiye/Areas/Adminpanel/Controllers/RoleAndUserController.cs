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
    public class RoleAndUserController : Controller
    {
        string cs = LoginLogoutController.cs;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [UserRoleAuthorizeFilter]
        public ActionResult Index()
        {
            return View(select_with_join());
        }
         
        [UserRoleAuthorizeFilter]
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(cs)) 
            {
                conn.Open();
                string query = "DELETE User_Id_And_Role_Id WHERE p_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Insert()
        { 
            return View(select());
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Insert(FormCollection form)
        {
            string[] _roles_id = form["role_id"].Split(',');
            int _user_id = Convert.ToInt32(form["user_id"]);

            foreach (string role_id in _roles_id)
            {
                Convert.ToInt32(role_id);
                using(SqlConnection conn =new SqlConnection(cs))
                {
                    conn.Open();
                    string query = "INSERT INTO User_Id_And_Role_Id (p_user_id, p_role_id) VALUES (" + _user_id + ", " + role_id + ")";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return RedirectToAction("Index");
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Update(int id)
        {  
            ViewBag.roles = select(id);
            dt.Clear();
            ViewBag.user_id = id;
            return View(select());
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Update(int id, FormCollection form)
        {
            string[] roles_id = form["role_id"].Split(','); 
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "DELETE  FROM User_Id_And_Role_Id WHERE p_user_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
             
            using (SqlConnection conn = new SqlConnection(cs)) 
            {
                foreach (string item in roles_id)
                {
                    conn.Open();
                    string query = "INSERT INTO User_Id_And_Role_Id (p_user_id, p_role_id) VALUES (" + id + ", " + item + ")";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();

                }
            }

            return RedirectToAction("Index");
        }


        RoleAndUserVM select()
        {
            RoleAndUserVM vm = new RoleAndUserVM();
            List<Users> _users = new List<Users>();
            List<Role> _roles = new List<Role>();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Users"; 
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }


            foreach (DataRow row in dt.Rows)
            {
                _users.Add(new Users()
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    user_username = row["user_username"].ToString(),
                });
            }

            dt.Clear();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Roles";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                _roles.Add(new Role()
                {
                    role_id = Convert.ToInt32(row["role_id"]),
                    role_name = row["role_name"].ToString()
                });
            }

            vm.roles = _roles;
            vm.users = _users;
            return vm;
        }
        
        List<RoleAndUser> select_with_join()
        {
            List<RoleAndUser> role_and_user = new List<RoleAndUser>();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT p_id, user_id, role_id, user_username, user_email, user_img, role_name FROM User_Id_And_Role_Id INNER JOIN Users ON user_id = p_user_id INNER JOIN Roles ON role_id = p_role_id";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                role_and_user.Add(new RoleAndUser()
                {
                    p_id = Convert.ToInt32(row["p_id"]),
                    user_id = Convert.ToInt32(row["user_id"]),
                    role_id = Convert.ToInt32(row["role_id"]),
                    user_username = row["user_username"].ToString(),
                    user_email = row["user_email"].ToString(),
                    user_img = row["user_img"].ToString(),
                    role_name = row["role_name"].ToString()
                }); 
            }
            return role_and_user;
        }
         
        List<int> select(int id)
        {
            List<int> _roles_id = new List<int>();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT  p_role_id  FROM Roles INNER JOIN User_Id_And_Role_Id ON role_id = p_role_id INNER JOIN Users ON user_id = p_user_id WHERE user_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                int _role_id = Convert.ToInt32(row["p_role_id"]);
                _roles_id.Add(_role_id);
            }
            return _roles_id;
        }


    }
}