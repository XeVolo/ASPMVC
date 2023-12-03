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
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: SaleAnnouncementModels
		public ActionResult Index()
		{
			var saleAnnouncementModels = db.SaleAnnouncements.Include(s => s.Product).Include(s => s.User);
			return View(saleAnnouncementModels.ToList());
		}
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
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}