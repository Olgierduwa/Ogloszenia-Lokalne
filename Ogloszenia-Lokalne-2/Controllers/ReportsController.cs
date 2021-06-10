using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ogloszenia_Lokalne_2.Models;

namespace Ogloszenia_Lokalne_2.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Reports.ToList());
        }

        // GET: Reports/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create(int id)
        {
            Report model = new Report();
            var userId = User.Identity.GetUserId();

            model.UserReportedID = userId;
            model.AdID = id;
            ViewBag.Ad = db.Ads.Find(id).Title;

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Report model)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Ad = db.Ads.Find(model.AdID).Title;
            return View(model);
        }

        // GET: Ads/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAd(int ReportID, int AdID)
        {
            if (AdID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (db.Ads.Find(AdID) == null)
            {
                return RedirectToAction("NotExist", "Ads");
            }

            Ad ad = db.Ads.Find(AdID);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("DeleteAd")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAdConfirmed(int ReportID, int AdID)
        {
            Ad ad = db.Ads.Find(AdID);
            db.Ads.Remove(ad);
            db.SaveChanges();

            Report report = db.Reports.Find(ReportID);
            db.Reports.Remove(report);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Reports/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
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
