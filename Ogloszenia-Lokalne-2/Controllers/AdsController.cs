using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ogloszenia_Lokalne_2.Models;
using Ogloszenia_Lokalne_2.Models.ViewModels;

namespace PROJECT_MVC.Controllers
{
    [Authorize]
    public class AdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ads
        public ActionResult Index()
        {
            string UserID = HttpContext.User.Identity.GetUserId();
            var Ads = db.Ads.Where(p => p.OwnerID == UserID);
            return View(Ads.ToList());
        }

        // GET: Ads/Details/5
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

            if (ad == null)
            {
                return HttpNotFound();
            }

            return View(ad);
        }

        // GET: Ads/Create
        public ActionResult Create()
        {
            var categories = db.Categories.Select(c => new { CategoryID = c.CategoryID, CategoryName = c.Name }).ToList();
            ViewBag.Categories = new MultiSelectList(categories, "CategoryID", "CategoryName");

            List<Category> categories2 = db.Categories.Select(c => c).ToList();
            AdViewModel adv = new AdViewModel() { Categories = categories2 };
            adv.Ad = new Ad();
            adv.SelectedCategories = db.Categories.Where(x => x.Name == "BRAK").Select(c => c.CategoryID).ToList();

            return View(adv);
        }

        // POST: Ads/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdViewModel adCreate)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.User.Identity.GetUserId();

                Ad adNew = new Ad
                {
                    AddingDate = DateTime.Now,
                    Title = adCreate.Ad.Title,
                    Price = adCreate.Ad.Price,
                    Views = 0,
                    IsActive = adCreate.Ad.IsActive,
                    IsValuable = adCreate.Ad.IsValuable,
                    Content = adCreate.Ad.Content,
                    References = adCreate.Ad.References,
                    Image = adCreate.Ad.Image,
                    ImageUpload = adCreate.Ad.ImageUpload,
                    OwnerID = UserID
                };

                if (adNew.ImageUpload != null)
                {
                    var fileName = Path.GetFileName(adNew.ImageUpload.FileName);
                    adNew.Image = "~/Images/" + fileName;
                    adNew.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
                }

                db.Ads.Add(adNew);
                db.SaveChanges();

                AdUser up = new AdUser { UserID = UserID, AdID = adNew.AdID };
                db.AdsUsers.Add(up);
                db.SaveChanges();

                db.Users.Where(x => x.Id == UserID).FirstOrDefault().AdsUsers.Add(up);

                foreach (int categoryID in adCreate.SelectedCategories)
                {
                    AdCategory pc = new AdCategory { CategoryID = categoryID, AdID = adNew.AdID };
                    db.AdsCategories.Add(pc);
                    db.SaveChanges();

                    adNew.AdsCategories.Add(pc);
                    db.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault().AdsCategories.Add(pc);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adCreate);
        }

        // GET: Ads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if(db.Ads.Find(id) == null)
            {
                return RedirectToAction("NotExist");
            }

            List<Category> categories = db.Categories.Select(c => c).ToList();
            AdViewModel adv = new AdViewModel() { Ad = db.Ads.Find(id), Categories = categories };
            adv.SelectedCategories = adv.Ad.AdsCategories.Select(c => c.CategoryID).ToList();

            if (adv == null)
            {
                return HttpNotFound();
            }

            return View(adv);
        }

        public ActionResult NotExist()
        {
            return View();
        }


        // POST: Ads/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdViewModel adEditView)
        {
            if (ModelState.IsValid)
            {
                Ad adNew = adEditView.Ad;
                int adID = adEditView.Ad.AdID;
                var AdsCategoriesList = db.AdsCategories.Where(cc => cc.AdID == adID).ToList();

                adNew.ImageUpload = adEditView.Ad.ImageUpload;
                if (adNew.ImageUpload != null)
                {
                    var fileName = Path.GetFileName(adNew.ImageUpload.FileName);
                    adNew.Image = "~/Images/" + fileName;
                    adNew.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
                }

                foreach (var AdCategory in AdsCategoriesList)
                {
                    db.AdsCategories.Remove(AdCategory);
                    db.SaveChanges();
                    db.Categories.Where(x => x.CategoryID == AdCategory.CategoryID)
                        .FirstOrDefault().AdsCategories.Remove(AdCategory);
                }
                adNew.AdsCategories = new List<AdCategory>();
                foreach (var categoryID in adEditView.SelectedCategories)
                {
                    AdCategory pc = new AdCategory { CategoryID = categoryID, AdID = adID };
                    db.AdsCategories.Add(pc);
                    db.SaveChanges();

                    adNew.AdsCategories.Add(pc);
                    db.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault().AdsCategories.Add(pc);
                }

                db.Entry(adNew).State = EntityState.Modified;
                db.SaveChanges();


                var userId = User.Identity.GetUserId();
                if (db.Users.Find(userId).Roles.First().RoleId == "a8a51adf-f0df-4a14-a7b8-5844a99339da")
                    return RedirectToAction("Index","Reports");
                else
                    return RedirectToAction("Index");
            }
            return View(adEditView);
        }

        // GET: Ads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ad ad = db.Ads.Find(id);
            db.Ads.Remove(ad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
