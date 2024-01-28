using ASPMVC.Models;
using ASPMVC.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: SaleAnnouncementModels
		public ActionResult Index(string searchString)
		{
			//Session["RequestID"] = Guid.NewGuid().ToString();
			//IncreaseVisitCount();
            if (Session["RequestID"] == null) 
            {
                string requestID = Guid.NewGuid().ToString(); //nadawanie nowego identyfikatora sesji
                Session["RequestID"] = requestID;
                IncreaseVisitCount();
            }
            int visitCountFromSession = GetVisitCount();
            ViewBag.VisitCount = visitCountFromSession;

			var saleAnnouncementModels = db.SaleAnnouncements
				.Where(s => s.Quantity > 0)
				.Include(s => s.Product)
				.Where(s => s.Product.IsDeleted == false)
				.Where(s=>s.State!=SaleAnnouncementState.Suspended)
				.Include(s => s.User)
				.Include (s => s.FilePaths)
				.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                saleAnnouncementModels = db.SaleAnnouncements.Where(s => s.Title.Contains(searchString)).ToList();
            }

            return View(saleAnnouncementModels);
		}

		public ActionResult NewsPromotions()
		{
			var query = db.SpecialOfferts.ToList();

			DateTime currentDate = DateTime.Now.AddDays(-10);
			var saleAnnouncementModels = db.SaleAnnouncements				
				.Where(s => s.Quantity > 0)
				.Include(s => s.Product)
				.Where(s => s.Product.IsDeleted == false)
				.Include(s => s.User)
				.Where(s => s.State != SaleAnnouncementState.Suspended)
				.Where(s => s.Date >= currentDate)
				.ToList();


            var specialoffer = db.SpecialOfferts
				.Where(s => s.ExpirationDate >= DateTime.Today)
				.Select(s => s.SaleAnnouncement)
				.Where(s => s.Quantity > 0)
				.Include(s => s.Product)
				.Where(s => s.Product.IsDeleted == false)
				.Include(s => s.User)
				.Where(s => s.State != SaleAnnouncementState.Suspended)
				.ToList();

            var combinedList = saleAnnouncementModels.Union(specialoffer).ToList();

			return View(combinedList);
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
			foreach (var i in query1)
			{
				i.ClientId = db.Users.Find(i.ClientId).Name;
			}
			ViewBag.Opinions = query1;

			var query2 = db.SpecialOfferts.Where(x => x.SaleAnnouncementId == id).Where(x => x.ExpirationDate >= DateTime.Today).ToList();
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
		public ActionResult Details(int? id, int? nicxD)
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
        private void IncreaseVisitCount()
        {
            int visitCount = GetVisitCount();
            visitCount++;
            SetVisitCount(visitCount);
        }

        private int GetVisitCount()
        {
            if (HttpContext.Cache["VisitCount"] == null)
                HttpContext.Cache["VisitCount"] = 0;

            return (int)HttpContext.Cache["VisitCount"];
        }

        private void SetVisitCount(int visitCount)
        {
            HttpContext.Cache["VisitCount"] = visitCount;
        }
    }
}