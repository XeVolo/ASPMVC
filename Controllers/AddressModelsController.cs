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
    public class AddressModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AddressModels
        public ActionResult Index()
        {
            return View(db.Addresses.ToList());
        }

        // GET: AddressModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel addressModel = db.Addresses.Find(id);
            if (addressModel == null)
            {
                return HttpNotFound();
            }
            return View(addressModel);
        }

        // GET: AddressModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ZipCode,City,StreetAndBuildingNumber,ApartmentNumber")] AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(addressModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(addressModel);
        }

        // GET: AddressModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel addressModel = db.Addresses.Find(id);
            if (addressModel == null)
            {
                return HttpNotFound();
            }
            return View(addressModel);
        }

        // POST: AddressModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ZipCode,City,StreetAndBuildingNumber,ApartmentNumber")] AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addressModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addressModel);
        }

        // GET: AddressModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel addressModel = db.Addresses.Find(id);
            if (addressModel == null)
            {
                return HttpNotFound();
            }
            return View(addressModel);
        }

        // POST: AddressModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddressModel addressModel = db.Addresses.Find(id);
            db.Addresses.Remove(addressModel);
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
