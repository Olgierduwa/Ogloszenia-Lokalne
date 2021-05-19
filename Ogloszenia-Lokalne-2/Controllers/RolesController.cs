using Ogloszenia_Lokalne_2.Models;
using System.Web.Mvc;

namespace PROJECT_MVC.Controllers
{
    public class RolesController : Controller
    {
        public string Create()
        {
            IdentityManager im = new IdentityManager();

            im.CreateRole("admin");
            im.CreateRole("user");

            return "OK";
        }


        public string AddToRole()
        {
            IdentityManager im = new IdentityManager();

            im.AddUserToRoleByUsername("olgierd@kuczynski.com", "admin");
            im.AddUserToRoleByUsername("weronika@zochowiec.com", "user");

            return "OK";
        }
    }
}