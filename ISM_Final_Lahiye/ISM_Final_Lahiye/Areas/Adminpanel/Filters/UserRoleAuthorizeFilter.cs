using ISM_Final_Lahiye.Areas.Adminpanel.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using ISM_Final_Lahiye.Areas.Adminpanel.Public;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Filters
{
    public class UserRoleAuthorizeFilter : ActionFilterAttribute
    {
        public static string Controller_name { get; set; }
        public static string Action_name { get; set; }
        public static string permission_warning { get; set; } = null;




        string cs = LoginLogoutController.cs;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

         
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller_name = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            Action_name = filterContext.ActionDescriptor.ActionName;

            if (!Check_user_role(Permission_id(Controller_name, Action_name)))
            {
                permission_warning = "Dear \""+LoginLogoutController.Logined_user.user_username+"\" you can not enter this page";
                filterContext.Result = new RedirectResult("/Adminpanel/Dashboard/Index");
                return;
            }  
            base.OnActionExecuting(filterContext);
        }


        int Permission_id(string controller_name, string action_name)
        {
            dt.Clear();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT per_id FROM Permissions WHERE per_controller = '" + controller_name + "' AND per_action = '" + action_name + "'";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            } 
                int id = Convert.ToInt32(dt.Rows[0]["per_id"]);
                return id;
           
        }

        List<int> Roles_id(int id)
        {
            dt.Clear();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT role_id FROM User_Id_And_Role_Id INNER JOIN Users ON p_user_id = user_id INNER JOIN Roles ON p_role_id = role_id WHERE user_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            List<int> _roles_id = new List<int>();
            foreach (DataRow row in dt.Rows)
            {
                _roles_id.Add(Convert.ToInt32(row["role_id"]));
            }
            return _roles_id;
        }

        bool Check_user_role(int per_id)
        {
            dt.Clear();
            List<int> roles_id = Roles_id(LoginLogoutController.Logined_user.user_id);
            int permission_id = Permission_id(Controller_name, Action_name);

            foreach (int item in roles_id)
            {
                using (SqlConnection conn = new SqlConnection(cs)) 
                {
                    string query = "SELECT p_id, per_controller, per_action FROM Permission_Id_And_Role_Id inner join Permissions ON per_id = p_permission_id WHERE p_permission_id = " + per_id + " and p_role_id = " + item;
                    SqlCommand com = new SqlCommand(query, conn);
                    da.SelectCommand = com;
                    da.Fill(dt);
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (row["per_controller"].ToString() == Controller_name && row["per_action"].ToString() == Action_name) 
                    {
                        dt.Clear();
                        return true;
                    }
                }
                dt.Clear();
            }
            return false;
        }
    }
}