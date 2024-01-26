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
    public class BasketModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BasketModels
        public ActionResult Index()
        {
            var basketModels = db.Baskets.Include(b => b.User);
			return View(basketModels.ToList());
        }

        // GET: BasketModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketModel basketModel = db.Baskets.Find(id);
            if (basketModel == null)
            {
                return HttpNotFound();
            }
			List<ProductModel> products = new List<ProductModel>();
			var query1 = db.BasketConnectors.Where(x => x.BasketId==basketModel.Id).ToList();
            foreach(var item in query1)
            {
                var query3 = db.Products.Find(item.ProductId);
                products.Add(query3);
            }			
			ViewBag.Products = products;
			return View(basketModel);
        }

        // GET: BasketModels/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: BasketModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId")] BasketModel basketModel)
        {
            if (ModelState.IsValid)
            {
                db.Baskets.Add(basketModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", basketModel.ClientId);
            return View(basketModel);
        }

        // GET: BasketModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketModel basketModel = db.Baskets.Find(id);
            if (basketModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", basketModel.ClientId);
            return View(basketModel);
        }

        // POST: BasketModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId")] BasketModel basketModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basketModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", basketModel.ClientId);
            return View(basketModel);
        }

        // GET: BasketModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketModel basketModel = db.Baskets.Find(id);
            if (basketModel == null)
            {
                return HttpNotFound();
            }
            return View(basketModel);
        }

        // POST: BasketModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BasketModel basketModel = db.Baskets.Find(id);
            db.Baskets.Remove(basketModel);
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
