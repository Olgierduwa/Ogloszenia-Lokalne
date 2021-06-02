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
        private int CountAdsPerSite = 3;
        private int CountSite;

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int id = 1)
        {
            id = CheckID(id);
            AdIndexViewModel AI = new AdIndexViewModel
            {
                SiteID = id,
                Filtr = false,
                FromPrice = 0,
                ToPrice = 0,
                SiteCount = CountSite
            };
            AI.Categories = db.Categories.Select(c => c).ToList();
            AI.SelectedCategories = db.Categories.Where(x => x.Name == "BRAK").Select(c => c.CategoryID).ToList();

            List<Ad> Ads = db.AdsUsers.Where(p => p.Ad.IsActive == true).Select(c => c.Ad).ToList();
            return Show(AI, Ads);
        }

        [HttpPost]
        public ActionResult Index(AdIndexViewModel AI, string id)
        {
            ;
            return Show(AI, GetAds(AI));
        }

        public ActionResult Search(AdIndexViewModel AdIndexViewModel)
        {
            return Show(AdIndexViewModel, GetAds(AdIndexViewModel));
        }
        [HttpPost]
        public ActionResult Filter(AdIndexViewModel AdIndexViewModel, string id)
        {
            ;
            return Show(AdIndexViewModel, GetAds(AdIndexViewModel));
        }
        public ActionResult Unfilter(AdIndexViewModel AI)
        {
            return Show(AI, GetAds(AI));
        }
        public ActionResult NextSite(AdIndexViewModel AI)
        {
            ;
            AI.SiteID = CheckID(AI.SiteID + 1);
            return Show(AI, GetAds(AI));
        }
        public ActionResult PreviousSite(AdIndexViewModel AI)
        {
            AI.SiteID = CheckID(AI.SiteID - 1);
            return Show(AI, GetAds(AI));
        }

        private ActionResult Show(AdIndexViewModel AI, List<Ad> Ads)
        {
            List<Ad> AdsPerSite = new List<Ad>();
            int StartID = (AI.SiteID - 1) * CountAdsPerSite;
            AI.AdCount = Ads.Count();
            for (int i = StartID; i < StartID + CountAdsPerSite; i++) if (i < AI.AdCount) AdsPerSite.Add(Ads[i]);
            ViewBag.Ads = AdsPerSite;
            var categories = db.Categories.Select(c => new { CategoryID = c.CategoryID, CategoryName = c.Name }).ToList();
            ViewBag.Categories = new MultiSelectList(categories, "CategoryID", "CategoryName");
            return View(viewName: "Index", AI);
        }
        private List<Ad> GetAds(AdIndexViewModel AI)
        {
            List<Ad> Ads = db.AdsUsers.Where(p => p.Ad.IsActive == true).Select(c => c.Ad).ToList();
            if(AI.Search != "")
            {

            }
            if(AI.Filtr)
            {

            }
            return Ads;
        }

        private int CheckID(int id)
        {
            int CountAds = db.AdsUsers.Where(p => p.Ad.IsActive == true).Count();
            CountSite = (CountAds + CountAdsPerSite - 1) / CountAdsPerSite;
            if (id < 1) id = 1;
            if (id > (CountAds + CountAdsPerSite - 1) / CountAdsPerSite) id = CountSite;
            return id;
        }

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