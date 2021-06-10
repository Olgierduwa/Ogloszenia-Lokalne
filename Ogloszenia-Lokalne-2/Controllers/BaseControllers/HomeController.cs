using Microsoft.AspNet.Identity;
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
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            string RoleName = "";
            if(user != null) RoleName = db.Roles.Find(user.Roles.First().RoleId).Name;
            ViewBag.RoleName = RoleName;
            int CountAds = db.AdsUsers.Where(p => p.Ad.IsActive == true).Count();
            id = CheckID(id, CountAds);

            AdIndexViewModel AI = new AdIndexViewModel
            {
                SiteID = id,
                Filtr = true,
                FromPrice = 0,
                ToPrice = 0,
                Search = "",
                SiteCount = CountSite,
                Messages = db.Messages.ToList()
            };
            AI.SelectedCategories = db.Categories.Where(x => x.Name == "BRAK").Select(c => c.CategoryID).ToList();

            return Show(AI);
        }

        [HttpPost]
        public ActionResult Index(AdIndexViewModel AI)
        {
            return Show(AI);
        }
        public ActionResult NextSite(AdIndexViewModel AI)
        {
            int CountAds = db.AdsUsers.Where(p => p.Ad.IsActive == true).Count();
            AI.SiteID = CheckID(AI.SiteID + 1, CountAds);
            return Show(AI);
        }
        public ActionResult PreviousSite(AdIndexViewModel AI)
        {
            int CountAds = db.AdsUsers.Where(p => p.Ad.IsActive == true).Count();
            AI.SiteID = CheckID(AI.SiteID - 1, CountAds);
            return Show(AI);
        }

        private ActionResult Show(AdIndexViewModel AI)
        {
            int AdCount = AI.AdCount;
            List<Ad> Ads = GetAds(AI);
            AI.AdCount = Ads.Count();
            if(AdCount != AI.AdCount)
                AI.SiteID = CheckID(1, AI.AdCount);
            AI.SiteID = CheckID(AI.SiteID, AI.AdCount);
            CountSite = (AI.AdCount + CountAdsPerSite - 1) / CountAdsPerSite;

            if (!AI.Filtr)
                AI.SelectedCategories = db.Categories.Where(x => x.Name == "BRAK").Select(c => c.CategoryID).ToList();
            AI.Categories = db.Categories.Select(c => c).ToList();
            AI.Messages = db.Messages.ToList();
            AI.SiteCount = CountSite;

            List<Ad> AdsPerSite = new List<Ad>();
            int StartID = (AI.SiteID - 1) * CountAdsPerSite;
            if(StartID >= 0)
                for (int i = StartID; i < StartID + CountAdsPerSite; i++) if (i < AI.AdCount) AdsPerSite.Add(Ads[i]);
            ViewBag.Ads = AdsPerSite;

            return View(viewName: "Index", AI);
        }
        private List<Ad> GetAds(AdIndexViewModel AI)
        {
            var query = db.Ads.Where(p => p.IsActive == true);

            List<Ad> ads = query.ToList();
            if (AI.Filtr)
            {
                if (AI.Search != null && AI.Search != "")
                    query = query.Where(m => m.Content.Contains(AI.Search) || m.Title.Contains(AI.Search));

                if (AI.ToPrice > 0 && query != null && query.Count() > 0)
                    query = query.Where(m => m.Price >= AI.FromPrice && m.Price <= AI.ToPrice);

                Category category = db.Categories.Find(AI.SelectedCategories.FirstOrDefault());
                if(category != null && category.Name != "BRAK")
                {
                    List<Ad> ads2 = query.ToList();
                    ads = new List<Ad>();
                    foreach(var ad in ads2)
                    {
                        foreach(var cat in ad.AdsCategories)
                        {
                            if(cat.CategoryID == category.CategoryID)
                            {
                                ads.Add(ad);
                                break;
                            }
                        }
                    }
                }
                else ads = query.ToList();
            }

            return ads;
        }
        private int CheckID(int id, int CountAds)
        {
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