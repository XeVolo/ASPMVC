using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPMVC.Models;

namespace ASPMVC.Controllers
{
    public class DeliveryMethodsModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeliveryMethodsModels
        public ActionResult Index()
        {
            return View(db.DeliveryMethods.ToList());
        }

        // GET: DeliveryMethodsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryMethodsModel deliveryMethodsModel = db.DeliveryMethods.Find(id);
            if (deliveryMethodsModel == null)
            {
                return HttpNotFound();
            }
            return View(deliveryMethodsModel);
        }

        // GET: DeliveryMethodsModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryMethodsModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price")] DeliveryMethodsModel deliveryMethodsModel)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryMethods.Add(deliveryMethodsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliveryMethodsModel);
        }

        // GET: DeliveryMethodsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryMethodsModel deliveryMethodsModel = db.DeliveryMethods.Find(id);
            if (deliveryMethodsModel == null)
            {
                return HttpNotFound();
            }
            return View(deliveryMethodsModel);
        }

        // POST: DeliveryMethodsModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price")] DeliveryMethodsModel deliveryMethodsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryMethodsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deliveryMethodsModel);
        }

        // GET: DeliveryMethodsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryMethodsModel deliveryMethodsModel = db.DeliveryMethods.Find(id);
            if (deliveryMethodsModel == null)
            {
                return HttpNotFound();
            }
            return View(deliveryMethodsModel);
        }

        // POST: DeliveryMethodsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryMethodsModel deliveryMethodsModel = db.DeliveryMethods.Find(id);
            db.DeliveryMethods.Remove(deliveryMethodsModel);
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
