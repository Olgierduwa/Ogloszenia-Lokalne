using Ogloszenia_Lokalne_2.Models;
using Ogloszenia_Lokalne_2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ogloszenia_Lokalne_2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var ads = db.Ads.Where(p => p.IsActive == true);
            return View(ads.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Category> categories = db.Categories.Select(c => c).ToList();
            AdViewModel ad = new AdViewModel() { Ad = db.Ads.Find(id), Categories = categories };
            ad.SelectedCategories = ad.Ad.AdsCategories.Select(c => c.CategoryID).ToList();
            ViewBag.Categories = db.AdsCategories.Where(x => x.AdID == ad.Ad.AdID).Select(c => c.Category).ToList();

            db.Ads.Where(a => a.AdID == ad.Ad.AdID).FirstOrDefault().Views++;
            db.SaveChanges();

            if (ad == null)
            {
                return HttpNotFound();
            }

            return View(ad);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}