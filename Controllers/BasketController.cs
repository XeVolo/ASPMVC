using ASPMVC.Models;
using ASPMVC.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
	public class BasketController : Controller
	{

		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Basket
		[Authorize]
		public ActionResult BasketView()
		{

			List<BasketViewModel> products2 = new List<BasketViewModel>();

			HttpCookie cookie = Request.Cookies["CartCookie"];

			if (cookie != null)
			{
				string cartContent = cookie.Value.ToString();
				List<KeyValuePair<string, int>> cartItems = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(cartContent);

				foreach (var id2 in cartItems)
				{

					int id = Convert.ToInt32(id2.Key);
					var query1 = db.SaleAnnouncements.Where(x => x.ProductId == id).FirstOrDefault();
					ProductModel prod = db.Products.Find(id);
					if (id2.Value > query1.Quantity)
					{
						TempData["InsufficientStock"] = "Brak wystarczającej ilości produktu na magazynie. " + prod.Name + " Dostępna ilość: " + query1.Quantity;
					}
					BasketViewModel prod3 = new BasketViewModel { ProductId = prod.Id, Name = prod.Name, Price = (prod.Price) * id2.Value, Quantity = id2.Value };
					products2.Add(prod3);
				}
			}
			else
			{
				ViewBag.Message = "Ciasteczko nie istnieje";
				return View(products2); ;
			}


			int promotion = 0;
			for (int i = 0; i < products2.Count; i++)
			{
				var id3 = products2[i];
				var query1 = db.SaleAnnouncements.Where(x => x.ProductId == id3.ProductId).ToList();
				if (query1.Count == 1)
				{
					int idc = query1[0].Id;
					var query2 = db.SpecialOfferts.Where(x => x.SaleAnnouncementId == idc).Where(x => x.ExpirationDate >= DateTime.Today).ToList();
					if (query2.Count == 1)
					{
						promotion = query2[0].PromotionValue;
						id3.Price = id3.Price - (id3.Price * promotion * 0.01);

					}
				}
			}
			return View(products2);
		}
		public ActionResult BasketViewDeleted(int? id)
		{
			List<BasketViewModel> products2 = new List<BasketViewModel>();

			HttpCookie cookie = Request.Cookies["CartCookie"];

			if (cookie != null)
			{
				string idc = Convert.ToString(id);
				string cartContent = cookie.Value.ToString();
				List<KeyValuePair<string, int>> cartItems = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(cartContent);

				for (int i = 0; i < cartItems.Count; i++)
				{
					if (cartItems[i].Key == idc)
					{
						if (cartItems[i].Value > 1)
						{
							cartItems[i] = new KeyValuePair<string, int>(cartItems[i].Key, cartItems[i].Value - 1);
						}
						else
						{
							cartItems.RemoveAt(i);
						}
						break;
					}
				}
				string cartData = JsonConvert.SerializeObject(cartItems);
				cookie.Value = cartData;
				Response.Cookies.Add(cookie);
			}
			else
			{
				ViewBag.Message = "Ciasteczko nie istnieje";
				return View(products2); ;
			}
			return RedirectToAction("BasketView");
		}


		[Authorize]
		public ActionResult Order()
		{

			List<ProductModel> products2 = new List<ProductModel>();

			HttpCookie cookie = Request.Cookies["CartCookie"];

			if (cookie != null)
			{
				string cartContent = cookie.Value.ToString();
				List<KeyValuePair<string, int>> cartItems = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(cartContent);

				foreach (var id2 in cartItems)
				{
					int id1 = Convert.ToInt32(id2.Key);
					ProductModel prod = db.Products.Find(id1);
					ProductModel prod3 = new ProductModel { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, IsDeleted = prod.IsDeleted };
					for (int i = 0; i < id2.Value; i++)
					{
						products2.Add(prod3);
					}
				}
			}
			if (products2.Count < 1)
			{
				return RedirectToAction("Index", "Home");
			}
			int promotion = 0;
			foreach (var id3 in products2)
			{
				var query1 = db.SaleAnnouncements.Where(x => x.ProductId == id3.Id).ToList();
				if (query1.Count == 1)
				{
					int idc = query1[0].Id;
					var query2 = db.SpecialOfferts.Where(x => x.SaleAnnouncementId == idc).Where(x => x.ExpirationDate >= DateTime.Today).ToList();
					if (query2.Count == 1)
					{
						promotion = query2[0].PromotionValue;
						id3.Price = id3.Price - (id3.Price * promotion * 0.01);
					}
				}
			}

			IdentityManager im = new IdentityManager();
			string id = HttpContext.User.Identity.Name;
			var query = db.Users.Where(u => u.UserName == id).ToList();
			var order = new OrderModel { DateTime = DateTime.Today, TotalPrice = 0, ClientId = query[0].Id, State=OrderState.InProgress };
			db.Orders.Add(order);
			db.SaveChanges();
			List<OrderProduct> orderProducts = new List<OrderProduct>();

			foreach (var item in products2)
			{
				var help = orderProducts.Find(x => x.ProductId == item.Id);
				if (help == null)
				{
					var orderprod = new OrderProduct { ProductId = item.Id, OrderId = order.Id, Price = item.Price, Quantity = 1, TotalPrice = item.Price };
					orderProducts.Add(orderprod);
				}
				else
				{
					help.Quantity = help.Quantity + 1;
					help.TotalPrice = help.Quantity * help.Price;
				}
			}



			foreach (var item3 in orderProducts)
			{
				var query6 = db.SaleAnnouncements.Where(x => x.ProductId == item3.ProductId).FirstOrDefault();
				query6.Quantity = query6.Quantity - item3.Quantity;
				if (query6.Quantity < 0)
				{
					query6.Quantity = 0;
				}

				db.Entry(query6).State = EntityState.Modified;
				db.SaveChanges();
			}
			double totalprice = 0;
			foreach (var item1 in orderProducts)
			{
				var orderprod = new OrderProduct { ProductId = item1.ProductId, OrderId = item1.OrderId, Price = item1.Price, Quantity = item1.Quantity, TotalPrice = item1.TotalPrice };
				totalprice += item1.TotalPrice;
				db.OrderProducts.Add(orderprod);
				db.SaveChanges();
			}
			order.TotalPrice = totalprice;
			db.Entry(order).State = EntityState.Modified;
			db.SaveChanges();

			cookie = Request.Cookies["CartCookie"];
			cookie.Expires = DateTime.Now.AddDays(-1);
			Response.Cookies.Add(cookie);
			return View();
		}

	}


}