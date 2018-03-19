using ISM_Final_Lahiye.Areas.Adminpanel.Controllers;
using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using ISM_Final_Lahiye.Areas.Adminpanel.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Filters
{
    public class UserIsLoginFilter : ActionFilterAttribute
    {
        Users user = LoginLogoutController.Logined_user;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (user.user_password == null || user.user_email == null || user.user_username == null)
            {
                filterContext.Result = new RedirectResult("/LoginLogout/Login");
                return;
            }
            if (HttpContext.Current.Session["username"] == null || HttpContext.Current.Session["password"] == null)
            {
                filterContext.Result = new RedirectResult("/LoginLogout/Login");
                return;
            }
            if (Settings.Hash_Password(user.user_username) != HttpContext.Current.Session["username"].ToString() || Settings.Hash_Password(user.user_password) != HttpContext.Current.Session["password"].ToString())
            {
                filterContext.Result = new RedirectResult("/LoginLogout/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}