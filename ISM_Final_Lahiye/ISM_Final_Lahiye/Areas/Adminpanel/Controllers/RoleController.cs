using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class RoleController : Controller
    {
        string cs = LoginLogoutController.cs;
        DataTable dt = new DataTable();

        [UserRoleAuthorizeFilter]
        public ActionResult Index()
        { 
            return View(select());
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Delete(int? id)
        { 
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string query = "DELETE [Roles] WHERE [role_id] = " + id;
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["delete_error_message"] = "You can not delete this Role because this Role connected to some users or to some permissions";
                return RedirectToAction("Index");
            }
            
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Insert(FormCollection form)
        {
            if (!insert(form["role_name"])) 
            {
                TempData["error"] = "Do not empty !!!";
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
        public ActionResult Update(int id, Role role)
        {  
            if (update(id, role))
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Do not empty";
                return View(select(id));
            }
        }

         bool insert(string r_name)
        {
            if (r_name == "") 
            {
                return false;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string query = "INSERT INTO [Roles] ([role_name]) VALUES ('" + r_name + "')";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            }
        }

         List<Role> select()
        { 
            List<Role> roles =new List<Role>();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM [Roles]";
                SqlCommand com = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {       
                roles.Add(new Role()
                {
                    role_id = Convert.ToInt32(row["role_id"]),
                    role_name = row["role_name"].ToString()
                });
            }
            return roles;
        }

         Role select(int id)
        {
            Role role = new Role();
            using(SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM [Roles] WHERE role_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                role.role_id = Convert.ToInt32(dt.Rows[0]["role_id"]);
                role.role_name = dt.Rows[0]["role_name"].ToString();
            } 
            return role;
        }

         bool update(int id, Role role)
        { 
            if (role.role_name == null) 
            {
                return false;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string query = "UPDATE [Roles] SET [role_name] = '" + role.role_name + "' WHERE role_id = " + id;
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                } 
                return true;
            }
        }
    }
}