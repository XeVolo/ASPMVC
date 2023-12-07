using Microsoft.Ajax.Utilities;
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

			var query1 = db.Opinions.Where(x => x.SaleAnnouncementId == id).ToList();
			foreach(var i in query1)
			{
				i.ClientId = db.Users.Find(i.ClientId).Name;
			}
			ViewBag.Opinions = query1;
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

		[HttpPost]
		public ActionResult Details(int? id,int ?nicxD)
		{
			List<int> products=new List<int>();
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			SaleAnnouncementModel saleAnnouncementModel = db.SaleAnnouncements.Find(id);
			if (saleAnnouncementModel == null)
			{
				return HttpNotFound();
			}
			var productid= Convert.ToString(saleAnnouncementModel.ProductId);
			HttpCookie cookie;

			if (!string.IsNullOrEmpty(productid))
			{
				cookie = Request.Cookies["a"];
				if(cookie == null)
				{
					cookie = new HttpCookie("a");
					cookie.Value = productid;
					Response.Cookies.Add(cookie);
				}
				else
				{
					string xd = Request.Cookies["a"].Value; 
					var idlist = xd.Split(' ');
					for (int i = 0; i <= idlist.Length-1;i++)
					{
						int id1 = Convert.ToInt32(idlist[i]);
						products.Add(id1);
					}
					products.Add(Convert.ToInt32(productid));
					string resoult = "";
					foreach (var id2 in products)
					{
						resoult = resoult + id2 + " ";
					}
					cookie.Value = resoult;
				}
				
				Response.Cookies.Add(cookie);
			}


			return View(saleAnnouncementModel);
		}
	}
}