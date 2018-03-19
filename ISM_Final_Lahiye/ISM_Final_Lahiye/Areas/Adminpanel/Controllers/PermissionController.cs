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
    public class PermissionController : Controller
    {
        Permission permission = new Permission();

        string cs = LoginLogoutController.cs;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [UserRoleAuthorizeFilter]
        public ActionResult Index()
        {
            return View(select());
        }


        [UserRoleAuthorizeFilter]
        public ActionResult Update(int id)
        {
            return View(select(id));
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Update(int id, Permission permission)
        {
            update(id, permission);
            return RedirectToAction("Index");
        }
         
        List<Permission> select()
        {
            List<Permission> permissions = new List<Permission>();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT * FROM [Permissions]";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                permissions.Add(new Permission()
                {
                    per_id = Convert.ToInt32(row["per_id"]),
                    per_link =row["per_link"].ToString(),
                    per_controller = row["per_controller"].ToString(),
                    per_action = row["per_action"].ToString()
                });
            }
            return permissions;
        }

        Permission select(int id)
        {
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT * FROM [Permissions] WHERE per_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            permission.per_id = Convert.ToInt32(dt.Rows[0]["per_id"]);
            permission.per_link = dt.Rows[0]["per_link"].ToString();
            return permission;
        }

        void update(int id, Permission permission)
         {
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "UPDATE [Permissions] SET [per_link] = '" + permission.per_link + "' WHERE [per_id] = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
         }
    }
}