using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROJECT_MVC.Models;

namespace PROJECT_MVC.Controllers
{
    public class PostersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posters
        public ActionResult Index()
        {
            var posters = db.Posters.Include(p => p.Owner);
            return View(posters.ToList());
        }

        // GET: Posters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = db.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }
            return View(poster);
        }

        // GET: Posters/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: Posters/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PosterID,Title,IsActive,IsValuable,Price,Content,References,Views,OwnerID")] Poster poster)
        {
            if (ModelState.IsValid)
            {
                db.Posters.Add(poster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.ApplicationUsers, "Id", "Email", poster.OwnerID);
            return View(poster);
        }

        // GET: Posters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = db.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.ApplicationUsers, "Id", "Email", poster.OwnerID);
            return View(poster);
        }

        // POST: Posters/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PosterID,Title,IsActive,IsValuable,Price,Content,References,Views,OwnerID")] Poster poster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.ApplicationUsers, "Id", "Email", poster.OwnerID);
            return View(poster);
        }

        // GET: Posters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = db.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }
            return View(poster);
        }

        // POST: Posters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poster poster = db.Posters.Find(id);
            db.Posters.Remove(poster);
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
