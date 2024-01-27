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
    public class PaymentMethodsModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentMethodsModels
        public ActionResult Index()
        {
            return View(db.PaymentMethods.ToList());
        }

        // GET: PaymentMethodsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethodsModel paymentMethodsModel = db.PaymentMethods.Find(id);
            if (paymentMethodsModel == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethodsModel);
        }

        // GET: PaymentMethodsModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethodsModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PaymentMethodsModel paymentMethodsModel)
        {
            if (ModelState.IsValid)
            {
                db.PaymentMethods.Add(paymentMethodsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentMethodsModel);
        }

        // GET: PaymentMethodsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethodsModel paymentMethodsModel = db.PaymentMethods.Find(id);
            if (paymentMethodsModel == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethodsModel);
        }

        // POST: PaymentMethodsModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PaymentMethodsModel paymentMethodsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentMethodsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentMethodsModel);
        }

        // GET: PaymentMethodsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethodsModel paymentMethodsModel = db.PaymentMethods.Find(id);
            if (paymentMethodsModel == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethodsModel);
        }

        // POST: PaymentMethodsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentMethodsModel paymentMethodsModel = db.PaymentMethods.Find(id);
            db.PaymentMethods.Remove(paymentMethodsModel);
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
