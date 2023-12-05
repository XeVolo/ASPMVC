using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemyBazDanychP1.Models;

namespace SystemyBazDanychP1.Controllers
{
	public class BasketController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Basket
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
					products2.Add(prod);
				}			
			}
			else
			{
				ViewBag.Message = "Ciasteczko [a] nie istnieje";
				return View(products2); ;
			}

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
                    products2.Add(prod);
                }
            }
            IdentityManager im = new IdentityManager();
            string id = HttpContext.User.Identity.Name;
            var order = new OrderModel { DateTime=DateTime.Today,TotalPrice=0,ClientId=id,Status="Przetwarzane" };
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




            return View(); ;
		}

	}
}