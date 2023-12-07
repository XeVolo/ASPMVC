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
    public class OpinionModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OpinionModels
        public ActionResult Index()
        {
            var opinionModels = db.Opinions.Include(o => o.SaleAnnouncement).Include(o => o.User);
            return View(opinionModels.ToList());
        }

        // GET: OpinionModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpinionModel opinionModel = db.Opinions.Find(id);
            if (opinionModel == null)
            {
                return HttpNotFound();
            }
            return View(opinionModel);
        }

        // GET: OpinionModels/Create
        [Authorize]
        public ActionResult Create(int? id)
        {
			//id ogloszenia
			IdentityManager im = new IdentityManager();
			string userid = HttpContext.User.Identity.Name;
			var query = db.Users.Where(u => u.UserName == userid).ToList();

            var query2=db.SaleAnnouncements.Where(u => u.Id == id).ToList();

			ViewBag.SaleAnnouncementId = new SelectList(query2, "Id", "Title");
            ViewBag.ClientId = new SelectList(query, "Id", "Name");
            return View();
        }

        // POST: OpinionModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,SaleAnnouncementId,Description")] OpinionModel opinionModel)
        {
            if (ModelState.IsValid)
            {
                db.Opinions.Add(opinionModel);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.SaleAnnouncementId = new SelectList(db.SaleAnnouncements, "Id", "SellerId", opinionModel.SaleAnnouncementId);
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", opinionModel.ClientId);
            return View(opinionModel);
        }

        // GET: OpinionModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpinionModel opinionModel = db.Opinions.Find(id);
            if (opinionModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleAnnouncementId = new SelectList(db.SaleAnnouncements, "Id", "SellerId", opinionModel.SaleAnnouncementId);
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", opinionModel.ClientId);
            return View(opinionModel);
        }

        // POST: OpinionModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId,SaleAnnouncementId,Description")] OpinionModel opinionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opinionModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SaleAnnouncementId = new SelectList(db.SaleAnnouncements, "Id", "SellerId", opinionModel.SaleAnnouncementId);
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", opinionModel.ClientId);
            return View(opinionModel);
        }

        // GET: OpinionModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpinionModel opinionModel = db.Opinions.Find(id);
            if (opinionModel == null)
            {
                return HttpNotFound();
            }
            return View(opinionModel);
        }

        // POST: OpinionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpinionModel opinionModel = db.Opinions.Find(id);
            db.Opinions.Remove(opinionModel);
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
