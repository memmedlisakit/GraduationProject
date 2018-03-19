using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class RoleAndPermissionController : Controller
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
            delete(id);
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
            int role_id = Convert.ToInt32(form["p_role_id"]);
            string[] permissions_id =  form["p_permission_id"].Split(',');

           
            using(SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                foreach (var per_id in permissions_id)
                {
                    Convert.ToInt32(per_id);
                    string query = "INSERT INTO Permission_Id_And_Role_Id (p_role_id, p_permission_id) VALUES (" + role_id + ", " + per_id + ")";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                }
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
        public ActionResult Update(int id, FormCollection form)
        { 
            string[] permissions_id = form["p_permission_id"].Split(',');

            using (SqlConnection conn = new SqlConnection(cs)) 
            {
                conn.Open();
                string query = "DELETE Permission_Id_And_Role_Id WHERE p_role_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                foreach (var per_id in permissions_id)
                {
                    Convert.ToInt32(per_id);
                    string query = "INSERT INTO Permission_Id_And_Role_Id (p_role_id, p_permission_id) VALUES (" + id + ", " + per_id + ")";
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                }
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        void delete(int id)
        {
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "DELETE Permission_Id_And_Role_Id WHERE p_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
        }
         
        RoleAndPermissionVM select()
        {
            RoleAndPermissionVM vm = new RoleAndPermissionVM();
            List<Role> _roles = new List<Role>();
            List<Permission> _permissions = new List<Permission>();
            using(SqlConnection conn =new SqlConnection(cs))
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
            dt.Clear(); 
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Permissions";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            } 
            foreach (DataRow row in dt.Rows)
            {
                _permissions.Add(new Permission()
                {
                    per_id = Convert.ToInt32(row["per_id"]),
                    per_link = row["per_link"].ToString(),
                    per_controller = row["per_controller"].ToString(),
                    per_action = row["per_action"].ToString()
                });
            } 
            vm.roles = _roles;
            vm.permissions = _permissions; 
            return vm; 
        }

        RoleAndPermissionVM select_with_join()
        {
            RoleAndPermissionVM vm = new RoleAndPermissionVM();
            List<Permission> _permissions = new List<Permission>();
            List<Role> _roles = new List<Role>();
            List<PermissionAndRole> _permission_id_and_role_id = new List<PermissionAndRole>();

            using(SqlConnection conn=new SqlConnection(cs))
            {
                string query = "SELECT p_id, p_permission_id, p_role_id, per_link, role_name FROM Permissions INNER JOIN Permission_Id_And_Role_Id ON per_id = p_permission_id INNER JOIN Roles ON role_id = p_role_id";  
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow row  in dt.Rows)
            {
                _permissions.Add(new Permission()
                {
                    per_link = row["per_link"].ToString()
                });
                _roles.Add(new Role()
                {
                    role_name = row["role_name"].ToString()
                });
                _permission_id_and_role_id.Add(new PermissionAndRole()
                {
                    p_id = Convert.ToInt32(row["p_id"]),
                    p_permission_id = Convert.ToInt32(row["p_permission_id"]),
                    p_role_id = Convert.ToInt32(row["p_role_id"])
                });
            }
            vm.permissions = _permissions;
            vm.roles = _roles;
            vm.permission_id_and_role_id = _permission_id_and_role_id;
            return vm;
        }   

        RoleAndPermissionVM select(int id)
        {
            RoleAndPermissionVM vm = new RoleAndPermissionVM();
            Role _role = new Role();
            List<Permission> _permissions = new List<Permission>();
            List<PermissionAndRole> _permission_id_and_role_id = new List<PermissionAndRole>();


            using (SqlConnection conn = new SqlConnection(cs)) 
            {
                string query = "SELECT * FROM Roles WHERE role_id = " + id;
                SqlCommand comm = new SqlCommand(query, conn);
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            _role.role_id = Convert.ToInt32(dt.Rows[0]["role_id"]);
            _role.role_name = dt.Rows[0]["role_name"].ToString();

            dt.Clear();

            using (SqlConnection conn = new SqlConnection(cs)) 
            {
                string query = "SELECT * FROM Permissions";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                _permissions.Add(new Permission()
                {
                    per_id = Convert.ToInt32(row["per_id"]),
                    per_link = row["per_link"].ToString()
                });
            }

            dt.Clear();

            using (SqlConnection conn = new SqlConnection(cs)) 
            {
                string query = "SELECT p_permission_id FROM Permissions INNER JOIN Permission_Id_And_Role_Id ON per_id = p_permission_id WHERE p_role_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                _permission_id_and_role_id.Add(new PermissionAndRole()
                {
                    p_permission_id = Convert.ToInt32(row["p_permission_id"])
                });
            }


            vm.role = _role;
            vm.permissions = _permissions;
            vm.permission_id_and_role_id = _permission_id_and_role_id;
            return vm;
        }
    }
}