using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using ASPMVC.Models;
using Newtonsoft.Json;

namespace ASPMVC.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: SaleAnnouncementModels
		public ActionResult Index()
		{

			var saleAnnouncementModels = db.SaleAnnouncements.Where(s=>s.Quantity>0).Include(s => s.Product).Include(s => s.User).ToList();
			return View(saleAnnouncementModels);
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
			
            var query2 = db.SpecialOfferts.Where(x => x.SaleAnnouncementId == id ).Where(x=>x.ExpirationDate>=DateTime.Today).ToList();
			ViewBag.Promotion = query2;
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
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			SaleAnnouncementModel saleAnnouncementModel = db.SaleAnnouncements.Find(id);
			if (saleAnnouncementModel == null)
			{
				return HttpNotFound();
			}

			var productid = Convert.ToString(saleAnnouncementModel.ProductId);

			List<KeyValuePair<string, int>> itemsInCart;
			HttpCookie cookie = Request.Cookies["CartCookie"];


			if (cookie != null)
			{
				string existingCart = cookie.Value.ToString();
				itemsInCart = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(existingCart);
			}
			else
			{
				cookie = new HttpCookie("CartCookie");
				itemsInCart = new List<KeyValuePair<string, int>>();
			}


			bool productFound = false;
			for (int i = 0; i < itemsInCart.Count; i++)
			{
				if (itemsInCart[i].Key == productid)
				{
					itemsInCart[i] = new KeyValuePair<string, int>(itemsInCart[i].Key, itemsInCart[i].Value + 1);
					productFound = true;
					break;
				}
			}

			if (!productFound)
			{
				itemsInCart.Add(new KeyValuePair<string, int>(productid, 1));
			}


			string cartData = JsonConvert.SerializeObject(itemsInCart);
			cookie.Value = cartData;
			Response.Cookies.Add(cookie);

			var query1 = db.Opinions.Where(x => x.SaleAnnouncementId == id).ToList();
			foreach (var i in query1)
			{
				i.ClientId = db.Users.Find(i.ClientId).Name;
			}
			ViewBag.Opinions = query1;

			var query2 = db.SpecialOfferts.Where(x => x.SaleAnnouncementId == id).Where(x => x.ExpirationDate >= DateTime.Today).ToList();
			ViewBag.Promotion = query2;

			return View(saleAnnouncementModel);
		}
	}
}