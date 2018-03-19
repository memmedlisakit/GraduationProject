using System.Web.Mvc;

namespace ISM_Final_Lahiye.Areas.Adminpanel
{
    public class AdminpanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Adminpanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Adminpanel_default",
                "Adminpanel/{controller}/{action}/{id}",
                new { controller = "LoginLogout", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ISM_Final_Lahiye.Areas.Adminpanel.Controllers" }
            );
        }
    }
}