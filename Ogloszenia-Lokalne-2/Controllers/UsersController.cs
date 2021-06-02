using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ogloszenia_Lokalne_2.Models;
using Ogloszenia_Lokalne_2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Ogloszenia_Lokalne_2.Models.ApplicationDbContext;

namespace Ogloszenia_Lokalne_2.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User/Edit/5
        public ActionResult SetRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<IdentityRole> roles = db.Roles.Select(c => c).ToList();
            RoleViewModel RoleView = new RoleViewModel() { User = db.Users.Find(id), Roles = roles };
            RoleView.SelectedRoles = RoleView.User.Roles.Select(c => c.RoleId).ToList();

            if (RoleView.User == null)
            {
                return HttpNotFound();
            }
            return View(RoleView);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetRole(RoleViewModel RoleView)
        {
            if (ModelState.IsValid)
            {
                IdentityManager im = new IdentityManager();
                foreach(var RoleID in RoleView.SelectedRoles)
                    im.AddUserToRole(RoleView.User.Id, db.Roles.Find(RoleID).Name);
                return RedirectToAction("Index", "Roles");
            }
            return View(RoleView);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser User = db.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser User = db.Users.Find(id);
            List<AdUser> AU = new List<AdUser>();
            AU.AddRange( User.AdsUsers );
            foreach(var aduser in AU)
            {
                Ad ad = db.Ads.Find(aduser.AdID);
                db.Ads.Remove(ad);
            }
            db.Users.Remove(User);
            db.SaveChanges();
            return RedirectToAction("Index", "Roles");
        }
    }
}
