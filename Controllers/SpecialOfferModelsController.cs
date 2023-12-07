using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SystemyBazDanychP1.Models;

namespace SystemyBazDanychP1.Controllers
{
    public class SpecialOfferModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpecialOfferModels
        public ActionResult Index()
        {
            var specialOfferts = db.SpecialOfferts.Include(s => s.SaleAnnouncement);
            return View(specialOfferts.ToList());
        }

        // GET: SpecialOfferModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialOfferModel specialOfferModel = db.SpecialOfferts.Find(id);
            if (specialOfferModel == null)
            {
                return HttpNotFound();
            }
            return View(specialOfferModel);
        }

        // GET: SpecialOfferModels/Create
        public ActionResult Create()
        {
            ViewBag.SaleAnnouncementId = new SelectList(db.SaleAnnouncements, "Id", "Title");
            return View();
        }

        // POST: SpecialOfferModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SaleAnnouncementId,PromotionValue,ExpirationDate")] SpecialOfferModel specialOfferModel)
        {
            if (ModelState.IsValid)
            {
                db.SpecialOfferts.Add(specialOfferModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SaleAnnouncementId = new SelectList(db.SaleAnnouncements, "Id", "SellerId", specialOfferModel.SaleAnnouncementId);
            return View(specialOfferModel);
        }

        // GET: SpecialOfferModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialOfferModel specialOfferModel = db.SpecialOfferts.Find(id);
            if (specialOfferModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleAnnouncementId = new SelectList(db.SaleAnnouncements, "Id", "SellerId", specialOfferModel.SaleAnnouncementId);
            return View(specialOfferModel);
        }

        // POST: SpecialOfferModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SaleAnnouncementId,PromotionValue,ExpirationDate")] SpecialOfferModel specialOfferModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialOfferModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SaleAnnouncementId = new SelectList(db.SaleAnnouncements, "Id", "SellerId", specialOfferModel.SaleAnnouncementId);
            return View(specialOfferModel);
        }

        // GET: SpecialOfferModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialOfferModel specialOfferModel = db.SpecialOfferts.Find(id);
            if (specialOfferModel == null)
            {
                return HttpNotFound();
            }
            return View(specialOfferModel);
        }

        // POST: SpecialOfferModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecialOfferModel specialOfferModel = db.SpecialOfferts.Find(id);
            db.SpecialOfferts.Remove(specialOfferModel);
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
