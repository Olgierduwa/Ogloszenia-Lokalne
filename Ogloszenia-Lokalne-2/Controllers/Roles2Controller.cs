using Ogloszenia_Lokalne_2.Models;
using System.Web.Mvc;
using static Ogloszenia_Lokalne_2.Models.ApplicationDbContext;

namespace PROJECT_MVC.Controllers
{
    public class Roles2Controller : Controller
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


        public string DelRoles()
        {
            IdentityManager im = new IdentityManager();

            im.ClearUserRoles("0a0bc44e-23c7-4e59-92d5-bda2f5fd4b26");

            return "OK";
        }
    }
}