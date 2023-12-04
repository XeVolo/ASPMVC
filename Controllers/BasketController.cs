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

				return View(products2);
			}
			else
			{
				ViewBag.Message = "Ciasteczko [a] nie istnieje";
			}
			return View();
		}

	}
}