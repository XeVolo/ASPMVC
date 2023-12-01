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
    public class SaleAnnouncementModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SaleAnnouncementModels
        public ActionResult Index()
        {
            var saleAnnouncementModels = db.SaleAnnouncements.Include(s => s.Product).Include(s => s.User);
            return View(saleAnnouncementModels.ToList());
        }

        // GET: SaleAnnouncementModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleAnnouncementModel saleAnnouncementModel = db.SaleAnnouncements.Find(id);
            if (saleAnnouncementModel == null)
            {
                return HttpNotFound();
            }
            return View(saleAnnouncementModel);
        }

        // GET: SaleAnnouncementModels/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: SaleAnnouncementModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SellerId,Title,Description,Quantity,ProductId,Status,Date")] SaleAnnouncementModel saleAnnouncementModel)
        {
            if (ModelState.IsValid)
            {
                db.SaleAnnouncements.Add(saleAnnouncementModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", saleAnnouncementModel.ProductId);
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Name", saleAnnouncementModel.SellerId);
            return View(saleAnnouncementModel);
        }

        // GET: SaleAnnouncementModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleAnnouncementModel saleAnnouncementModel = db.SaleAnnouncements.Find(id);
            if (saleAnnouncementModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", saleAnnouncementModel.ProductId);
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Name", saleAnnouncementModel.SellerId);
            return View(saleAnnouncementModel);
        }

        // POST: SaleAnnouncementModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SellerId,Title,Description,Quantity,ProductId,Status,Date")] SaleAnnouncementModel saleAnnouncementModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleAnnouncementModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", saleAnnouncementModel.ProductId);
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Name", saleAnnouncementModel.SellerId);
            return View(saleAnnouncementModel);
        }

        // GET: SaleAnnouncementModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleAnnouncementModel saleAnnouncementModel = db.SaleAnnouncements.Find(id);
            if (saleAnnouncementModel == null)
            {
                return HttpNotFound();
            }
            return View(saleAnnouncementModel);
        }

        // POST: SaleAnnouncementModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleAnnouncementModel saleAnnouncementModel = db.SaleAnnouncements.Find(id);
            db.SaleAnnouncements.Remove(saleAnnouncementModel);
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
