using PROJECT_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Ogloszenia_Lokalne.Models.ApplicationDbContext;

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