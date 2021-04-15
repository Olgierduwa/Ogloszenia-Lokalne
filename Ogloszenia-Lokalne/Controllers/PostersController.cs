using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ogloszenia_Lokalne.Models;
using PROJECT_MVC.Models;
using PROJECT_MVC.Models.ModelsView;

namespace PROJECT_MVC.Controllers
{
    public class PostersController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Posters
        [Authorize]
        public ActionResult Index()
        {
            string UserID = HttpContext.User.Identity.GetUserId();
            var posters = _context.Posters.Where(p => p.OwnerID == UserID);
            return View(posters.ToList());
        }

        // GET: Posters/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = _context.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }

            var query = poster.PostersCategories.Select(p => p.Category.Name);
            ViewBag.Categories = query.ToList();

            return View(poster);
        }

        // GET: Posters/Create
        [Authorize]
        public ActionResult Create()
        {
            var categories = _context.Categories.Select(c => new { CategoryID = c.CategoryID, CategoryName = c.Name }).ToList();
            ViewBag.Categories = new MultiSelectList(categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Posters/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(PosterCreate posterCreate)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.User.Identity.GetUserId();

                Poster posterNew = new Poster
                {
                    AddingDate = DateTime.Now,
                    Title = posterCreate.Poster.Title,
                    Price = posterCreate.Poster.Price,
                    Views = 0,
                    IsActive = posterCreate.Poster.IsActive,
                    IsValuable = posterCreate.Poster.IsValuable,
                    Content = posterCreate.Poster.Content,
                    References = posterCreate.Poster.References,
                    OwnerID = UserID
                };
                _context.Posters.Add(posterNew);
                _context.SaveChanges();

                UserPoster up = new UserPoster { ApplicationUserID = UserID, PosterID = posterNew.PosterID };
                _context.UsersPosters.Add(up);
                _context.SaveChanges();

                _context.Users.Where(x => x.Id == UserID).FirstOrDefault().UserPoster.Add(up);

                foreach (int categoryID in posterCreate.CategoriesID)
                {
                    PosterCategory pc = new PosterCategory {CategoryID = categoryID, PosterID = posterNew.PosterID };
                    _context.PostersCategories.Add(pc);
                    _context.SaveChanges();

                    posterNew.PostersCategories.Add(pc);
                    _context.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault().PostersCategories.Add(pc);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posterCreate);
        }

        // GET: Posters/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosterCreate poster = new PosterCreate() { Poster = _context.Posters.Find(id) };
            if (poster == null)
            {
                return HttpNotFound();
            }

            var categories = _context.Categories.Select(c => new { CategoryID = c.CategoryID, CategoryName = c.Name }).ToList();
            List<int> categoriesSelected = poster.Poster.PostersCategories.Select(c => c.CategoryID).ToList();
            ViewBag.Categories = new MultiSelectList(categories, "CategoryID", "CategoryName", selectedValues: categoriesSelected);

            return View(poster);
        }

        // POST: Posters/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(PosterCreate posterEditView)
        {
            if (ModelState.IsValid)
            {
                Poster posterNew = posterEditView.Poster;
                int posterID = posterEditView.Poster.PosterID;
                var PostersCategoriesList = _context.PostersCategories.Where(cc => cc.PosterID == posterID).ToList();

                foreach (var PosterCategory in PostersCategoriesList)
                {
                    _context.PostersCategories.Remove(PosterCategory);
                    _context.SaveChanges();
                    _context.Categories.Where(x => x.CategoryID == PosterCategory.CategoryID)
                        .FirstOrDefault().PostersCategories.Remove(PosterCategory);
                }
                posterNew.PostersCategories = new List<PosterCategory>();
                foreach (var categoryID in posterEditView.CategoriesID)
                {
                    PosterCategory pc = new PosterCategory { CategoryID = categoryID, PosterID = posterID };
                    _context.PostersCategories.Add(pc);
                    _context.SaveChanges();

                    posterNew.PostersCategories.Add(pc);
                    _context.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault().PostersCategories.Add(pc);
                }

                _context.Entry(posterNew).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posterEditView);
        }

        // GET: Posters/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = _context.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }
            return View(poster);
        }

        // POST: Posters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Poster poster = _context.Posters.Find(id);
            _context.Posters.Remove(poster);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
