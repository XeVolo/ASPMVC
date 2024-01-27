using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPMVC.Models;
using ASPMVC.Models.Enums;

namespace ASPMVC.Controllers
{
    public class SaleAnnouncementModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SaleAnnouncementModels
        public ActionResult Index()
        {
            IdentityManager im = new IdentityManager();
            string CiD = HttpContext.User.Identity.Name;
            var usersId = db.Users.Where(u => u.UserName == CiD).FirstOrDefault();

            var adminUsers = db.Users
            .Where(u => u.Roles.Any(ur => ur.RoleId == "d0d956b8-f0a1-4737-83b5-1b5f52c68010"))
            .ToList();
            if (adminUsers.Contains(usersId))
            {
                var saleAnnouncementModel = db.SaleAnnouncements.Include(s => s.Product).Include(s => s.User);
                return View(saleAnnouncementModel.ToList());
            }
            else
            {
                var saleAnnouncementModel = db.SaleAnnouncements.Where(s => s.SellerId == usersId.Id).Include(s => s.Product).Include(s => s.User);
                return View(saleAnnouncementModel.ToList());
            }
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
		[Authorize]
		public ActionResult Create()
        {
			IdentityManager im = new IdentityManager();
			string id = HttpContext.User.Identity.Name;
            var query =db.Users.Where(u => u.UserName == id).ToList();
	
			im.AddUserToRoleByUsername(id, "Seller");
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SellerId = new SelectList(query,"Id","Name");
            return View();
        }

        // POST: SaleAnnouncementModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnnouncementViewModel model, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {

				
				var product = new ProductModel { Name = model.Name, CategoryId = model.CategoryId, Price = model.Price, IsDeleted = false };
                db.Products.Add(product);
				db.SaveChanges();
				var announcement = new SaleAnnouncementModel { SellerId = model.SellerId, Title = model.Title, Description = model.Description, Quantity = model.Quantity, ProductId = product.Id, State=SaleAnnouncementState.Active, Date = model.Date};
                db.SaleAnnouncements.Add(announcement);
                db.SaveChanges();

                if (files != null && files.Any())
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                            file.SaveAs(filePath);

                            var imageFilePath = "Images/" + fileName;

                            var fileEntity = new FilePathsModel { Path = imageFilePath, SaleAnnouncementId = announcement.Id };
                            db.FilePaths.Add(fileEntity);
                        }
                    }

                    db.SaveChanges();
                }
                return RedirectToAction("Index","Home");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SellerId = new SelectList(db.Users, "Id", "Name", model.SellerId);
            return View(model);
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
