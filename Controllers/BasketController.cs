using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using SystemyBazDanychP1.Models;

namespace SystemyBazDanychP1.Controllers
{
	public class BasketController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Basket
		[Authorize]
		public ActionResult BasketView()
		{
			List<int> products = new List<int>();
			List<ProductModel> products2 = new List<ProductModel>();
			if (Request.Cookies["a"] != null)
			{
				//ViewBag.Message = Request.Cookies["a"].Value;


				string xd = Request.Cookies["a"].Value;
				var idlist = xd.Split(' ');
				for (int i = 0; i <= idlist.Length - 1; i++)
				{
					int id1 = Convert.ToInt32(idlist[i]);
					products.Add(id1);
				}								
				foreach (var id2 in products)
				{
					ProductModel prod= db.Products.Find(id2);
					ProductModel prod3 = new ProductModel { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, IsDeleted = prod.IsDeleted };
					products2.Add(prod3);
				}			
			}
			else
			{
				ViewBag.Message = "Ciasteczko [a] nie istnieje";
				return View(products2); ;
			}
			int promotion = 0;
			for(int i=0;i<products2.Count;i++)
			{
				var id3= products2[i];
				var query1 = db.SaleAnnouncements.Where(x=>x.ProductId==id3.Id).ToList();
				if (query1.Count == 1)
				{
					int idc = query1[0].Id;
					var query2 = db.SpecialOfferts.Where(x => x.SaleAnnouncementId == idc).Where(x => x.ExpirationDate >= DateTime.Today).ToList();
					if (query2.Count == 1) 
					{ 
						 promotion = query2[0].PromotionValue;
						id3.Price = id3.Price * promotion * 0.01;
					}
				}
			}
			IdentityManager im = new IdentityManager();
			string id = HttpContext.User.Identity.Name;
			var query = db.Users.Where(u => u.UserName == id).FirstOrDefault();
			var query4= db.Baskets.Where(x=>x.ClientId==query.Id).ToList();
			BasketModel basket = null;
			if (query4.Count==0) {
				 basket = new BasketModel { ClientId = query.Id };
				db.Baskets.Add(basket);
				db.SaveChanges();
			}
			else
			{
				 basket = query4[0];
			}

			foreach(var item in products2)
			{
				var historyprod=new BasketConnectorModel {ProductId= item.Id,BasketId=basket.Id};
				db.BasketConnectors.Add(historyprod);
				db.SaveChanges();
			}


			return View(products2);
		}
		[HttpPost]
		public ActionResult BasketView(int? id)
		{
			HttpCookie cookie = Request.Cookies["a"];
			cookie.Expires = DateTime.Now.AddDays(-1);
			Response.Cookies.Add(cookie);
			List<ProductModel> products2 = new List<ProductModel>();
			return View(products2);
		}


		[Authorize]


		public ActionResult Order()
		{
            //przycisk w koszyku zamów generuje order model i w nim order products 
            List<int> products = new List<int>();
            List<ProductModel> products2 = new List<ProductModel>();
            if (Request.Cookies["a"] != null)
            {
                string xd = Request.Cookies["a"].Value;
                var idlist = xd.Split(' ');
                for (int i = 0; i <= idlist.Length - 1; i++)
                {
                    int id1 = Convert.ToInt32(idlist[i]);
                    products.Add(id1);
                }
                foreach (var id2 in products)
                {
                    ProductModel prod = db.Products.Find(id2);
					ProductModel prod3 = new ProductModel { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, IsDeleted = prod.IsDeleted };
					products2.Add(prod3);
				}
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
						id3.Price = id3.Price * promotion * 0.01;
					}
				}
			}

			IdentityManager im = new IdentityManager();
            string id = HttpContext.User.Identity.Name;
            var query = db.Users.Where(u => u.UserName == id).ToList();
            var order = new OrderModel { DateTime = DateTime.Today, TotalPrice = 0, ClientId = query[0].Id,Status="Przetwarzane" };
            db.Orders.Add(order);
            db.SaveChanges();
			List <OrderProduct> orderProducts = new List<OrderProduct>();

			foreach(var item in products2)
			{
				var help=orderProducts.Find(x=>x.ProductId == item.Id);
				if (help == null)
				{
					var orderprod = new OrderProduct { ProductId=item.Id,OrderId=order.Id,Price=item.Price,Quantity=1,TotalPrice=item.Price};
					orderProducts.Add(orderprod);
				}
				else
				{
					help.Quantity = help.Quantity + 1;
					help.TotalPrice = help.Quantity * help.Price;
				}
			}
			double totalprice = 0;
			foreach(var item1 in orderProducts)
			{
                var orderprod = new OrderProduct { ProductId = item1.ProductId, OrderId = item1.OrderId, Price = item1.Price, Quantity = item1.Quantity, TotalPrice = item1.TotalPrice };
				totalprice += item1.TotalPrice;
				db.OrderProducts.Add(orderprod);
                db.SaveChanges();
            }
			order.TotalPrice = totalprice;
			db.Entry(order).State = EntityState.Modified;
			db.SaveChanges();


			return View(); ;
		}

	}
}